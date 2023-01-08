////using Microsoft.AspNetCore.Http;

////namespace Crud.Shared;
////public class MyFormFile : IFormFile
////{
////	private readonly Stream _stream;
////	private HeaderDictionary _headers;
////	public MyFormFile()
////	{
////		ContentType = string.Empty;
////		ContentDisposition = string.Empty;
////		Name = string.Empty;
////		FileName = string.Empty;
////		_headers = new HeaderDictionary();
////		_stream = Stream.Null;
////	}
////	public MyFormFile(Stream stream)
////	{
////		_stream = stream;
////		ContentType = string.Empty;
////		ContentDisposition = string.Empty;
////		Name = string.Empty;
////		FileName = string.Empty;
////		_headers = new HeaderDictionary();
////	}
////	public MyFormFile(string contentType, string contentDisposition, IHeaderDictionary headers, long length, string name, string fileName, Stream stream)
////	{
////		ContentType = contentType;
////		ContentDisposition = contentDisposition;
////		_headers = (HeaderDictionary)headers;
////		Length = length;
////		Name = name;
////		FileName = fileName;
////		_stream = stream;
////	}
////	public string ContentType { get; set; }

////	public string ContentDisposition { get; set; }

////	public IHeaderDictionary Headers
////	{
////		get { return _headers; }
////		set { _headers = (HeaderDictionary)value; }
////	}

////	public long Length { get; set; }

////	public string Name { get; set; }

////	public string FileName { get; set; }

////	public void CopyTo(Stream target)
////	{
////		_stream.CopyTo(target);
////	}

////	public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
////	{
////		await _stream.CopyToAsync(target, cancellationToken);
////	}

////	public Stream OpenReadStream()
////	{
////		return _stream;
////	}
////}
////using Microsoft.AspNetCore.Http;
////using Microsoft.Extensions.Primitives;
////using Newtonsoft.Json;

////namespace Crud.Shared
////{
////    public class MyFormFile : IFormFile
////    {
////        private readonly Stream _stream;
////        private Dictionary<string, StringValues> _headers;

////        [JsonConstructor]
////        public MyFormFile(string contentType, string contentDisposition, long length, string name, string fileName, Dictionary<string, StringValues> headers, Stream stream)
////        {
////            ContentType = contentType;
////            ContentDisposition = contentDisposition;
////            Length = length;
////            Name = name;
////            FileName = fileName;
////            _headers = headers;
////            _stream = stream;
////        }

////        public MyFormFile()
////        {
////            ContentType = string.Empty;
////            ContentDisposition = string.Empty;
////            Name = string.Empty;
////            FileName = string.Empty;
////            _headers = new Dictionary<string, StringValues>();
////            _stream = Stream.Null;
////        }

////        public string ContentType { get; set; }

////        public string ContentDisposition { get; set; }

////        [JsonIgnore]
////        public IHeaderDictionary Headers
////        {
////            get { return new HeaderDictionary(_headers); }
////            set { _headers = new Dictionary<string, StringValues>(value); }
////        }

////        [JsonProperty("Headers")]
////        private Dictionary<string, StringValues> HeadersDictionary
////        {
////            get { return _headers; }
////            set { _headers = value; }
////        }

////        public long Length { get; set; }

////        public string Name { get; set; }

////        public string FileName { get; set; }

////        public void CopyTo(Stream target)
////        {
////            _stream.CopyTo(target);
////        }

////        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
////        {
////            await _stream.CopyToAsync(target, cancellationToken);
////        }

////        public Stream OpenReadStream()
////        {
////            return _stream;
////        }
////    }
////}
////using Microsoft.AspNetCore.Http;
////using System.Collections.Generic;

////namespace Crud.Shared
////{
////    public class MyFormFile : IFormFile
////    {
////        private readonly Stream _stream;
////        private readonly IHeaderDictionary _headers;

////        public MyFormFile(Stream stream, IHeaderDictionary headers)
////        {
////            _stream = stream;
////            _headers = headers;
////            ContentType = string.Empty;
////            ContentDisposition = string.Empty;
////            Name = string.Empty;
////            FileName = string.Empty;
////        }

////        public string ContentType { get; set; }

////        public string ContentDisposition { get; set; }

////        public IHeaderDictionary Headers
////        {
////            get { return _headers; }
////        }

////        public long Length { get; set; }

////        public string Name { get; set; }

////        public string FileName { get; set; }

////        public void CopyTo(Stream target)
////        {
////            _stream.CopyTo(target);
////        }

////        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
////        {
////            await _stream.CopyToAsync(target, cancellationToken);
////        }

////        public Stream OpenReadStream()
////        {
////            return _stream;
////        }
////    }
////}
////using Microsoft.AspNetCore.Http;
////using Microsoft.Extensions.Primitives;
////using System.Collections.Generic;

////namespace Crud.Shared
////{
////    public class MyFormFile : IFormFile
////    {
////        private readonly Stream _stream;
////        private readonly HeaderDictionary _headers;

////        public MyFormFile(Stream stream, IHeaderDictionary headers)
////        {
////            _stream = stream;
////            _headers = new HeaderDictionary(headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
////            ContentType = string.Empty;
////            ContentDisposition = string.Empty;
////            Name = string.Empty;
////            FileName = string.Empty;
////        }
////        public MyFormFile(string contentType, string contentDisposition, long length, string name, string fileName, Dictionary<string, StringValues> headers, Stream stream)
////        {
////            _stream = stream;
////            _headers = new HeaderDictionary(headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
////            ContentType = contentType;
////            ContentDisposition = contentDisposition;
////            Name = name;
////            FileName = fileName;
////            Length = length;
////        }
////        public string ContentType { get; set; }

////        public string ContentDisposition { get; set; }

////        public IHeaderDictionary Headers
////        {
////            get { return _headers; }
////        }

////        public long Length { get; set; }

////        public string Name { get; set; }

////        public string FileName { get; set; }

////        public void CopyTo(Stream target)
////        {
////            _stream.CopyTo(target);
////        }

////        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
////        {
////            await _stream.CopyToAsync(target, cancellationToken);
////        }

////        public Stream OpenReadStream()
////        {
////            return _stream;
////        }
////    }
////}
////using Microsoft.AspNetCore.Http;
////using Microsoft.Extensions.Primitives;
////using Newtonsoft.Json;
////using System;
////using System.Collections.Generic;
////using System.IO;
////using System.Threading;
////using System.Threading.Tasks;

////namespace Crud.Shared
////{
////    public class MyFormFile : IFormFile
////    {
////        private readonly Stream _stream;
////        private readonly IHeaderDictionary _headers;

////        public MyFormFile(string contentType, string contentDisposition, long length, string name, string fileName, Dictionary<string, StringValues> headers, Stream stream)
////        {
////            _stream = stream;
////            _headers = new HeaderDictionary(headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
////            ContentType = contentType;
////            ContentDisposition = contentDisposition;
////            Name = name;
////            FileName = fileName;
////            Length = length;
////        }
////        public string ContentType { get; set; }

////        public string ContentDisposition { get; set; }

////        public IHeaderDictionary Headers => _headers;

////        public long Length { get; set; }

////        public string Name { get; set; }

////        public string FileName { get; set; }

////        public void CopyTo(Stream target)
////        {
////            _stream.CopyTo(target);
////        }

////        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
////        {
////            await _stream.CopyToAsync(target, cancellationToken);
////        }

////        public Stream OpenReadStream()
////        {
////            return _stream;
////        }
////    }
////}

using Microsoft.AspNetCore.Http;
namespace Crud.Shared;
public class MyFormFile
{

	private readonly Stream _baseStream;

	public string ContentDisposition
	{
		get;
		set;
	}

	public string ContentType
	{
		get;
		set;
	}

	public long Length
	{
		get;
	}

	public string Name
	{
		get;
	}

	public string FileName
	{
		get;
	}

	public MyFormFile(Stream baseStream, long length, string name, string fileName, string contentDisposition, string contentType)
	{
		_baseStream = baseStream;
		Length = length;
		Name = name;
		FileName = fileName;
		ContentDisposition = contentDisposition;
		ContentType = contentType;
	}
	public Stream OpenReadStream()
	{
		return _baseStream;
	}
	public void CopyTo(Stream target)
	{
		if (target == null)
		{
			throw new ArgumentNullException("target");
		}

		using (Stream stream = OpenReadStream())
		{
			stream.CopyTo(target, 81920);
		}
	}
	public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken))
	{
		if (target == null)
		{
			throw new ArgumentNullException("target");
		}

		using (Stream readStream = OpenReadStream())
		{
			await readStream.CopyToAsync(target, 81920, cancellationToken);
		}
	}
}