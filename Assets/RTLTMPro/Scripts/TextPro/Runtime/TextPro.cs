using TMPro;

#if UNITY_EDITOR && UNITY_LOCALIZATION
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Metadata;
using UnityEngine.Localization.Tables;
#endif

namespace Hexagon.UI
{
    public class TextPro : TextMeshProUGUI
    {
#if UNITY_EDITOR && UNITY_LOCALIZATION
        private TextProLocalizationCollection _localizationSettings;

        [ContextMenu("Localize TextPro")]
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
                    var methodDelegate =
                        System.Delegate.CreateDelegate(typeof(UnityAction<string>), this, setStringMethod) as
                            UnityAction<string>;
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(stringEvent.OnUpdateString,
                        methodDelegate);
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

                    //ValidateKeyIndex(table, keyIndex);

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
                    entry.Metadata.AddMetadata(new Comment { CommentText = GetTextPath(transform) });

                    stringEvent.StringReference = new LocalizedString
                        { TableReference = table.TableCollectionName, TableEntryReference = key };

                    EditorUtility.SetDirty(_localizationSettings);
                    EditorUtility.SetDirty(stringEvent);
                }
            }
        }


        private void ResolveLocalizationSettings()
        {
            var paths = AssetDatabase.FindAssets($"t:{nameof(TextProLocalizationCollection)}");
            if (paths is { Length: > 0 })
            {
                _localizationSettings =
                    AssetDatabase.LoadAssetAtPath<TextProLocalizationCollection>(
                        AssetDatabase.GUIDToAssetPath(paths[0]));
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