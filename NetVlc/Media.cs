using System;
using System.Runtime.InteropServices;

namespace NetVlc
{
    public class Media : IDisposable
    {
        #region DLL Calls
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr libvlc_media_new(IntPtr p_instance,
          [MarshalAs(UnmanagedType.LPStr)] string psz_mrl, ref libvlc_exception_t ex);

        [DllImport("libvlc")]
        static extern void libvlc_media_release(IntPtr p_meta_desc);
        #endregion

        private IntPtr mediaHandler;
        public IntPtr Handler
        {
            get { return this.mediaHandler; }
        }

        public Media(Core core, string path)
        {
            libvlc_exception_t ex = new libvlc_exception_t();
            this.mediaHandler = libvlc_media_new(core.Handler, path, ref ex);
            if (this.mediaHandler == IntPtr.Zero) throw new VlcException();
        }
        public void Dispose()
        {
            libvlc_media_release(this.mediaHandler);
        }
    }
}
