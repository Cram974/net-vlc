using System;
using System.Runtime.InteropServices;

namespace NetVlc
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct libvlc_exception_t
    {
        public int b_raised;
        public int i_code;
        [MarshalAs(UnmanagedType.LPStr)]
        public string psz_message;
    }

    class VlcException : Exception
    {
        #region DLL Calls
        [DllImport("libvlc")]
        public static extern void libvlc_clearerr();

        [DllImport("libvlc")]
        public static extern IntPtr libvlc_errmsg();
        #endregion

        protected string _err;

        public VlcException()
            : base()
        {
            IntPtr errorPointer = libvlc_errmsg();
            _err = errorPointer == IntPtr.Zero ? "VLC Exception"
                : Marshal.PtrToStringAuto(errorPointer);
        }

        public override string Message { get { return _err; } }
    }
}
