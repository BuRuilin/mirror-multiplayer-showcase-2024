using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Text;
using UnityEngine;

/// <summary>
/// 因为Mirror本身不支持地图选择，因此使用Http服务器专门用于发送地图信息
/// </summary>
public class HttpServer : IDisposable
{
	public static string scene;
	public static HttpListener Listener = new HttpListener();
	private static Thread thread;
	private static bool isStop = false;
	public static void Start(string ip)
	{
		Listener.Prefixes.Add($"http://+:26666/");
		Listener.Start();
		thread = new Thread(Listen);
		thread.Start();
	}

	public void Dispose()
	{
		Stop();
	}

	public static void Stop()
	{
		isStop = true;
		thread.Abort();
		Listener.Close();
	}

	public static void Listen()
	{
		while (!isStop)
		{
			var context = Listener.GetContext();
			context.Response.ContentEncoding = Encoding.UTF8;
			context.Response.StatusCode = 200;
			context.Response.OutputStream.Write(Encoding.UTF8.GetBytes(scene));
			context.Response.OutputStream.Close();
			context.Response.Close();
		}

	}
}