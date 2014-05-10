using System;
using System.Runtime.InteropServices;

namespace NetVlc
{
    public class Core : IDisposable
    {
        #region DLL Calls
        [DllImport("libvlc", CallingConvention=CallingConvention.Cdecl)]
        static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray,
          ArraySubType = UnmanagedType.LPStr)] string[] argv, ref libvlc_exception_t ex);

        [DllImport("libvlc")]
        static extern void libvlc_release(IntPtr instance);
        #endregion

        private IntPtr coreHandler;

        public IntPtr Handler
        {
            get { return this.coreHandler; }
        }

        public Core(string[] argv)
        {
            libvlc_exception_t ex = new libvlc_exception_t();
            this.coreHandler = libvlc_new(argv.Length, argv, ref ex);
            if (this.coreHandler == IntPtr.Zero) throw new VlcException();
        }

    
        public void  Dispose()
        {
            libvlc_release(this.coreHandler);
        }
    }
}
