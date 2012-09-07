using System;

namespace MVC {
    public abstract class AComparable : IEquatable<AComparable>, IComparable {
        public abstract bool Equals(AComparable to_me);

        public abstract int CompareTo(object id);

        public override abstract int GetHashCode();

        public static int compare(IComparable a, IComparable b) {
            if (a == null) {
                if (b == null)
                    return 0;
                else
                    return -1;
            } else {
                return a.CompareTo(b);
            }
        }

        public override bool Equals(object obj) {
            if (obj == null) {
                // We're obviously not null. Duh.
                return false;
            }

            if (obj.GetType() == this.GetType())
                return this.Equals(obj as AIdentifier);
            else
                throw new NotSupportedException("Cannot compare type " + this.GetType().ToString() + " to type " + obj.GetType().ToString());
        }

        public static bool operator ==(AComparable a, AComparable b) {
            // Checks if the 1st argument is null. Doy.
            if (object.ReferenceEquals(a, null)) {
                if (object.ReferenceEquals(b, null)) {
                    return true;
                } else {
                    return false;
                }
            }

            return a.Equals(b);
        }

        public static bool operator !=(AComparable a, AComparable b) {
            return !(a == b);
        }


    }
}
