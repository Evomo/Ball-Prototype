namespace Level.Interfaces {
	public interface ILevelEncodable<T> {
		string Encode2String();
		T FromString(string s);
	}
}