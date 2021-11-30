var MyPlugin = {
   IsMobile: function()
   {
      return UnityLoader.SystemInfo.mobile;
   }
};  
mergeInto(LibraryManager.library, MyPlugin);