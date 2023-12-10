using UnityEngine;
using UnityObject = UnityEngine.Object;
namespace CursedAmongUs
{
	public static class ReactorUsing
	{
		public static byte[] ReadFully(this System.IO.Stream input)
		{
			using System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
			input.CopyTo(memoryStream);
			return memoryStream.ToArray();
		}
		public static Sprite CreateSprite(this Texture2D texture, Vector2? pivot = null, float pixelsPerUnit = 100f)
		{
			return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot ?? new Vector2(0.5f, 0.5f), pixelsPerUnit);
		}
		public static void Destroy(this UnityObject obj)
		{
			UnityObject.Destroy(obj);
		}


	}
}
