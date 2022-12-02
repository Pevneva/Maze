using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageType CurrentLanguage { get; set; }

    private static readonly XmlDocument _root = new XmlDocument();

    private void Start()
    {
        Initialize();
        TryDownload();
    }

    public static string Text(string groupId, string stringId)
    {
        try
        {
            string xPath = ParamsController.Xml.BEFORE_GROUP_ID + groupId;
            xPath += ParamsController.Xml.BEFORE_STRING_ID + stringId;
            xPath += ParamsController.Xml.BEFORE_CURRENT_LANGUAGE + CurrentLanguage;
            xPath += ParamsController.Xml.AFTER_CURRENT_LANGUAGE;
            return _root.SelectSingleNode(xPath).InnerText;
        }
        catch (NullReferenceException)
        {
            return "Not been translated";
        }
    }

    private void Initialize()
    {
        CurrentLanguage = LanguageType.ru;
    }

    private void TryDownload()
    {
        if (Resources.Load(ParamsController.Xml.LANGUAGE_FILE_NAME) == false)
            throw new FileNotFoundException();

        _root.LoadXml(Resources.Load(ParamsController.Xml.LANGUAGE_FILE_NAME).ToString());

        if (_root.SelectSingleNode("Settings/meta/language").InnerText != "Multilingual")
            throw new XmlSchemaValidationException();
    }
}

public enum LanguageType
{
    en = 0,
    ru = 1
};