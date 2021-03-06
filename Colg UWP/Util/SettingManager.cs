﻿using System.Collections.Generic;
using Windows.Storage;

namespace Colg_UWP.Util
{
    public class SettingManager
    {
        private static readonly ApplicationDataContainer _rootContainer = ApplicationData.Current.LocalSettings;

        private static readonly ApplicationDataContainer _userPreference = _rootContainer.CreateContainer(ContainerNames.UserPreference,
            ApplicationDataCreateDisposition.Always);

        private static readonly ApplicationDataContainer _loginContainer = _rootContainer.CreateContainer(ContainerNames.Login,
            ApplicationDataCreateDisposition.Always);

        private static Dictionary<string, ApplicationDataContainer> ContianerMapper = new Dictionary
            <string, ApplicationDataContainer>()
        {
        };

        public static void Save<T>(string name, T value)
        {
            var propertySet = ContianerMapper[name].Values;
            if (propertySet.ContainsKey(name))
            {
                propertySet[name] = value;
            }
            else
            {
                propertySet.Add(name, value);
            }
        }

        public static T Read<T>(string name)
        {
            var propertySet = ContianerMapper[name].Values;
            if (propertySet.ContainsKey(name))
            {
                return (T)propertySet[name];
            }
            else
            {
                propertySet.Add(name, default(T));
                return default(T);
            }
        }
    }

    public class ContainerNames
    {
        public const string UserPreference = "UserPreference";
        public const string Login = "Login";
    }

    public class SettingNames
    {
    }
}