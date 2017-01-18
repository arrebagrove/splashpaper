namespace Tasks.Models {
    public sealed class Paper {
        private string _id { get; set; }
        private int _likes { get; set; }
        private string _urlRaw { get; set; }
        private string _urlRegular { get; set; }
        private string _thumbnail { get; set; }

        public string Id {
            get {
                return _id;
            }
            set {
                if (_id != value) {
                    _id = value;
                }
            }
        }

        public int Likes {
            get {
                return _likes;
            }
            set {
                if (_likes != value) {
                    _likes = value;
                }
            }
        }

        public string URLRaw {
            get {
                return _urlRaw;
            }
            set {
                if (_urlRaw != value) {
                    _urlRaw = value;
                }
            }
        }

        public string Thumbnail {
            get {
                return _thumbnail;
            }
            set {
                if (_thumbnail!=value) {
                    _thumbnail = value;
                }
            }
        }

        public string URLRegular {
            get {
                return _urlRegular;
            }

            set {
                if (_urlRegular != value) {
                    _urlRegular = value;
                }
            }
        }

    }
}
