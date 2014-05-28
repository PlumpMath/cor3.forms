using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace System.Cor3.Drawing
{
	[Obsolete("This class is depreceated—no longer intended for use.")]
	public class ImageLoader
	{
		public delegate void AssertUpdate(int index);
		readonly object threadLock = new object();
		// static public bool IsRunning = false;
		FloatPoint InterpolMax = new FloatPoint(300);
		
		#region Properties
		public Control Client { get; set; }
		public string ImagePath { get; set; }
		
		public DirectoryInfo DInfo { get; set; }
		public List<FileInfo> Files { get; set; }
		
		/// <summary>
		/// This is the main thumbnail dictionary where images are stored.
		/// </summary>
		public DICT<FileInfo, Bitmap> Thumbnails {
			get { return thumbnails; }
			set { thumbnails = value; }
		} DICT<FileInfo,Bitmap> thumbnails = new DICT<FileInfo, Bitmap>();
		
		public HPoint NodeSize {
			get { return nodeSize; }
			set { nodeSize = value; }
		} HPoint nodeSize = new HPoint(140,140);
		
		public HPoint ThumbSize {
			get { return thumbSize; }
			set { thumbSize = value; }
		} HPoint thumbSize = new HPoint(128);
		
		public HPoint RowSize {
			get { return new HPoint(Client.ClientSize)/nodeSize; }
		} Point PSize { get { return RowSize; } }
		
		public event AssertUpdate ThumbsUpdated;
		
		#endregion
		
		/// <summary>
		/// When triggered, indicates that a thumbnail image is ready for rendering.
		/// </summary>
		/// <param name="index"></param>
		protected virtual void OnThumbsUpdated(int index)
		{
			if (ThumbsUpdated != null)
				ThumbsUpdated(index);
		}
		
		bool HasExt(string ext)
		{
			switch (ext)
			{
				case ".bmp":
				case ".cur":
				case ".gif":
				case ".ico":
				case ".jpg":
				case ".jpeg":
				case ".png":
					return true;
				default:
					return false;
			}
		}
		
		public List<FileInfo> QueryFiles
		{
			get
			{
				List<FileInfo> list = new List<FileInfo>();
				if (DInfo.GetFiles().Length==0) return list;
				foreach (FileInfo fi in DInfo.GetFiles()) if (HasExt(fi.Extension)) list.Add(fi); return list;
			}
		}
		
		void ReadImages(FloatPoint sizeRequest)
		{
			Global.cstat(ConsoleColor.Green,"Loading Thumbnails {0}",sizeRequest);
			
			foreach (FileInfo fi in Files)
			{
				if (thumbnails.ContainsKey(fi)) thumbnails[fi].Dispose();
				if (thumbnails.ContainsKey(fi)) thumbnails[fi] = null;
			}
			
			thumbnails.Clear();
			
			int incr = 0;
			
			foreach (FileInfo fi in Files)
			{
				#if (CONSOLE)
				Global.stat( "loading {0}" , fi.Name );
				#endif
				
				System.Diagnostics.Debug.Print("loading {0}",fi.Name);
				
				using (Bitmap targetBitmap=new Bitmap(fi.FullName))
				{
					Point targetSize = FloatPoint.Fit( sizeRequest, targetBitmap.Size, 2 );
					if (targetSize.X <= 0) targetSize = sizeRequest;
					if (targetSize.Y <= 0) targetSize = sizeRequest;
					Bitmap renderBitmap = new Bitmap(targetSize.X,targetSize.Y);
					using (Graphics g = Graphics.FromImage(renderBitmap))
					{
						g.HighQuality	(InterpolMax,targetBitmap);
						g.Clear			(Color.Black);
						g.DrawImage		(targetBitmap,0,0,targetSize.X,targetSize.Y);
						lock (threadLock)
						{
							try
							{
								thumbnails.Add(fi,renderBitmap);
							}
							catch (Exception exception)
							{
								System.Diagnostics.Debug.Print("{0}",exception);
							}
						}
					}
				}
				
				OnThumbsUpdated(incr++);
			}
		}
		
		public ImageLoader(string path, Point newSize, bool waitForThreadStart, AssertUpdate eHandler)
		{
			ImagePath = path;
			nodeSize = newSize;
			if (!waitForThreadStart) BeginLoading();
			ThumbsUpdated -= eHandler;
			ThumbsUpdated += eHandler;
		}
		
		public void GetInfo(bool begin) { ReadFileInformaion(ImagePath,nodeSize,begin); }
		void ReadFileInformaion(string path, Point newSize, bool readImages)
		{
			DInfo = new DirectoryInfo(path);
			Files = QueryFiles;
			
			Global.cstat(ConsoleColor.Yellow,nodeSize);
			
			if (readImages) ReadImages(newSize);
		}
		public void BeginLoading() { ReadFileInformaion(ImagePath,nodeSize,true); }
	
	}
}
