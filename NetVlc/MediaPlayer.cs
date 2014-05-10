using System;
using System.Runtime.InteropServices;

namespace NetVlc
{
    public class MediaPlayer : IDisposable
    {
        #region DLL Calls
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr libvlc_media_player_new_from_media(IntPtr media, ref libvlc_exception_t ex);

        [DllImport("libvlc")]
        static extern void libvlc_media_player_release(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        static extern void libvlc_media_player_set_drawable(IntPtr player, IntPtr drawable, ref libvlc_exception_t ex);

        [DllImport("libvlc")]
        static extern IntPtr libvlc_media_player_get_media(IntPtr player, ref libvlc_exception_t ex);

        [DllImport("libvlc")]
        static extern void libvlc_media_player_set_media(IntPtr player, IntPtr media, ref libvlc_exception_t ex);

        [DllImport("libvlc")]
        static extern int libvlc_media_player_play(IntPtr player, ref libvlc_exception_t ex);

        [DllImport("libvlc")]
        static extern void libvlc_media_player_pause(IntPtr player, ref libvlc_exception_t ex);

        [DllImport("libvlc")]
        static extern void libvlc_media_player_stop(IntPtr player, ref libvlc_exception_t ex);
        #endregion

        private IntPtr mplayerHandler;
        public IntPtr Handler
        {
            get { return this.mplayerHandler; }
        }
        public IntPtr WindowHandler
        {
            set 
            {
                libvlc_exception_t ex = new libvlc_exception_t();
                libvlc_media_player_set_drawable(this.mplayerHandler, value, ref ex); 
            }
        }

        public MediaPlayer(Media media)
        {
            libvlc_exception_t ex = new libvlc_exception_t();
            this.mplayerHandler = libvlc_media_player_new_from_media(media.Handler, ref ex);
            //this.mplayerHandler = libvlc_media_player_new_from_media(media.Handler);
            if (this.mplayerHandler == IntPtr.Zero) throw new VlcException();
        }

        public void Play()
        {
            libvlc_exception_t ex = new libvlc_exception_t();
            libvlc_media_player_play(this.mplayerHandler, ref ex);
        }
        public void Pause()
        {
            libvlc_exception_t ex = new libvlc_exception_t();
            libvlc_media_player_pause(this.mplayerHandler, ref ex);
        }
        public void Stop()
        {
            libvlc_exception_t ex = new libvlc_exception_t();
            libvlc_media_player_stop(this.mplayerHandler, ref ex);
        }

        public void Dispose()
        {
            libvlc_media_player_release(mplayerHandler);
        }
    }
}
