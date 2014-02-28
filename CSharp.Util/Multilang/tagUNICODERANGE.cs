namespace MultiLanguage
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [StructLayout(LayoutKind.Sequential, Pack=2)]
    internal struct tagUNICODERANGE
    {
        public ushort wcFrom;
        public ushort wcTo;
    }
}
