namespace splashpaper.Models {
    public class Paper {
        private string _id { get; set; }
        private int _likes { get; set; }
        private string _urlRaw { get; set; }
        private string _thumbnail { get; set; }

        public string id {
            get {
                return _id;
            }
            set {
                if (_id != value) {
                    _id = value;
                }
            }
        }

        public int likes {
            get {
                return _likes;
            }
            set {
                if (_likes != value) {
                    _likes = value;
                }
            }
        }

        public string urlRaw {
            get {
                return _urlRaw;
            }
            set {
                if (_urlRaw != value) {
                    _urlRaw = value;
                }
            }
        }

        public string thumbnail {
            get {
                return _thumbnail;
            }
            set {
                if (_thumbnail!=value) {
                    _thumbnail = value;
                }
            }
        }
    }
}
