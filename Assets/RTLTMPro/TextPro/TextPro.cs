#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Localization;
  #endif

using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Metadata;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace YoYo.UI
{
    public enum NumberMode { Context, English, Persian };

    public abstract class TextPro : MonoBehaviour
    {
        public Action OnLanguageChanged;
        
        public string text
        {
            set
            {
                _text = value;
                SetText();
            }
            get
            {
                return _text;
            }
        }

        [TextArea(4, 5)]
        [SerializeField]
        protected string _text;

        public NumberMode _numberMode;

        public NumberMode NumberMode
        {
            set
            {
                _numberMode = value;
                SetText();
            }
            get => _numberMode;
        }

        private TextProLocalizationSettings _localizationSettings;

        protected virtual void Start()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        protected virtual void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        private void OnSelectedLocaleChanged(Locale newLocale)
        {
            OnLanguageChanged?.Invoke();
        }
        
        public virtual void SetText()
        {
        }

        public virtual void FindRequiredComponent()
        {
        }

        private void Reset()
        {
            FindRequiredComponent();
        }
        
        

        #if UNITY_EDITOR
        [ContextMenu("Localize")]
        private void Localize()
        {
            var stringEvent = GetComponent<LocalizeStringEvent>();
            if (stringEvent == null)
            {
                stringEvent = gameObject.AddComponent<LocalizeStringEvent>();
            }

            OverrideLocalization(stringEvent);

            if (stringEvent.OnUpdateString.GetPersistentEventCount() == 0)
            {
                var setStringMethod = GetType().GetProperty("text")?.GetSetMethod();
                if (setStringMethod != null)
                {
                    var methodDelegate = System.Delegate.CreateDelegate(typeof(UnityAction<string>), this, setStringMethod) as UnityAction<string>;
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(stringEvent.OnUpdateString, methodDelegate);
                }
            }

            EditorUtility.SetDirty(gameObject);
        }
        private void OverrideLocalization(LocalizeStringEvent stringEvent)
        {
            if (_localizationSettings == null)
            {
                ResolveLocalizationSettings();
            }

            if (_localizationSettings != null && _localizationSettings.OverrideLocalization)
            {
                if (stringEvent.StringReference == null || stringEvent.StringReference.IsEmpty)
                {
                    var table = _localizationSettings.StringTable;
                    var keyIndex = _localizationSettings.GetLocalizeKeyIndex();

                    ValidateKeyIndex(table, keyIndex);

                    var key = $"{table.name}_{keyIndex}";
                    var entry = table.SharedData.AddKey(key);

                    if (_localizationSettings.Locales != null)
                    {
                        foreach (var localeStr in _localizationSettings.Locales)
                        {
                            var stringTable = table.GetTable(localeStr) as StringTable;

                            if (stringTable != null)
                            {
                                stringTable.AddEntry(key, text);
                                EditorUtility.SetDirty(stringTable);
                            }
                        }
                    }

                    entry.Metadata = new MetadataCollection();
                    entry.Metadata.AddMetadata(new Comment() {CommentText = GetTextPath(transform)});

                    stringEvent.StringReference = new LocalizedString() {TableReference = table.TableCollectionName, TableEntryReference = key};
                    
                    EditorUtility.SetDirty(_localizationSettings);
                    EditorUtility.SetDirty(stringEvent);
                }
            }
        }
        private void ValidateKeyIndex(StringTableCollection table, int keyIndex)
        {
            var last = table.SharedData.Entries?.Last();
            if (last != null)
            {
                string lastKey = last.Key;
                string[] splitKey = lastKey.Split('_');
                if (splitKey.Length > 1)
                {
                    if (int.TryParse(splitKey[1], out int lastIndex))
                    {
                        if (lastIndex >= keyIndex)
                        {
                            throw new AggregateException($"Localization Settings key index isn't valid. index: {keyIndex}, lastIndex: {lastIndex}");
                        }
                    }
                }
            }
        }

        private void ResolveLocalizationSettings()
        {
            var paths = AssetDatabase.FindAssets($"t:{nameof(TextProLocalizationSettings)}");
            if (paths is {Length: > 0})
            {
                _localizationSettings = AssetDatabase.LoadAssetAtPath<TextProLocalizationSettings>(AssetDatabase.GUIDToAssetPath(paths[0]));
            }
        }

        private string GetTextPath(Transform trans)
        {
            var prefab = PrefabUtility.GetOutermostPrefabInstanceRoot(gameObject);
            var parentName = gameObject.scene.name;
            var postfix = ".unity";

            if (prefab != null)
            {
                parentName = prefab.name;
                postfix = ".prefab";
            }

            var sb = new StringBuilder();
            do
            {
                string tranName = trans.name;
                trans = trans.parent;

                if (trans == null && tranName.Equals(parentName))
                    break;

                sb.Insert(0, "/" + tranName);
            } while (trans != null);

            sb.Insert(0, parentName + postfix);
            return sb.ToString();
        }
        #endif
    }
}