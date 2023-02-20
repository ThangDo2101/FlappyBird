
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;
    [System.Serializable]
    public class Reference
    {
        public string name;
        public GameObject value;
    }

    [LuaCallCSharp]
    public class Behavior : MonoBehaviour
    {
        public TextAsset luaScript;
        public Reference[] references;
        internal static LuaEnv luaEnv = new LuaEnv();
        internal static float lastGCTime = 0;
        internal const float GCInterval = 1;//1 second 

        private Action luaStart;
        private Action luaUpdate;
        private Action luaOnDestroy;

        private LuaTable scriptEnv;

        void Awake()
        {
            scriptEnv = luaEnv.NewTable();

            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            scriptEnv.Set("self", this);
            foreach (var refer in references)
            {
                scriptEnv.Set(refer.name, refer.value);
            }

            luaEnv.DoString(luaScript.text, null, scriptEnv);

            Action luaAwake = scriptEnv.Get<Action>("awake");
            scriptEnv.Get("start", out luaStart);
            scriptEnv.Get("update", out luaUpdate);
            scriptEnv.Get("ondestroy", out luaOnDestroy);

            if (luaAwake != null)
            {
                luaAwake();
            }
        }

        void Start()
        {
            if (luaStart != null)
            {
                luaStart();
            }
        }

        void Update()
        {
            if (luaUpdate != null)
            {
                luaUpdate();
            }
            if (Time.time - Behavior.lastGCTime > GCInterval)
            {
                luaEnv.Tick();
                Behavior.lastGCTime = Time.time;
            }
        }

        void OnDestroy()
        {
            if (luaOnDestroy != null)
            {
                luaOnDestroy();
            }
            luaOnDestroy = null;
            luaUpdate = null;
            luaStart = null;
            references = null;
            scriptEnv.Dispose();
        }
    }
