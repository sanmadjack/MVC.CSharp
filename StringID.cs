﻿using System;

namespace MVC {
    public class StringID : AIdentifier {
        private String id;
        public StringID(String new_id) {
            id = new_id;
        }

        public override int CompareTo(object obj) {
            return this.id.CompareTo(obj.ToString());
        }
        public override bool Equals(AIdentifier to_me) {
            return this.id.Equals(to_me.ToString());
        }

        public override string ToString() {
            return id;
        }
    }
}
