namespace Questionaire.common.tool
{
    public interface ICache<T>
	{
		T Get(object key);

		void Clear(object key);

		void ClearAll();
	}
}
