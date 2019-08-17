using System;

namespace UnicodeBrowser.Models
{
	public sealed class CodePointRange : IEquatable<CodePointRange>
	{
        public int FirstCodePoint { get; }
        public int LastCodePoint { get; }

        public CodePointRange(int firstCodePoint, int lastCodePoint)
        {
            FirstCodePoint = firstCodePoint;
            LastCodePoint = lastCodePoint;
		}

		public override bool Equals(object obj) => Equals(obj as CodePointRange);

		public bool Equals(CodePointRange other)
			=> other != null &&
				FirstCodePoint == other.FirstCodePoint &&
				LastCodePoint == other.LastCodePoint;

		public override int GetHashCode()
		{
			var hashCode = 1053634452;
			hashCode = hashCode * -1521134295 + FirstCodePoint.GetHashCode();
			hashCode = hashCode * -1521134295 + LastCodePoint.GetHashCode();
			return hashCode;
		}
	}
}
