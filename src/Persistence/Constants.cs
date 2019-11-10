namespace Doctrina.Persistence
{
    public class Constants
    {
        public const int OBJECT_TYPE_LENGTH = 12;

        private const int MAX_PATH_LENGTH = 2048;
        private const int MAX_SCHEME_LENGTH = 32;
        public const int MAX_URL_LENGTH = MAX_PATH_LENGTH + MAX_SCHEME_LENGTH + 3;

        /// <summary>
        /// The length of a MD5 hash, if 4 bits per char
        /// </summary>
        public static int HASH_LENGTH = 32;

        /// <summary>
        /// The length of a SHA1 hash, if 4 bits per char (160/4)
        /// </summary>
        public static int SHA1_HASH_LENGTH = 40;
    }
}
