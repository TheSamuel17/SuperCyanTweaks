namespace SuperCyanTweaks
{
    public static class EclipseRevampedCompat
    {
        private static bool? _enabled;

        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Nuxlar.EclipseRevamped");
                }
                return (bool)_enabled;
            }
        }
    }
}
