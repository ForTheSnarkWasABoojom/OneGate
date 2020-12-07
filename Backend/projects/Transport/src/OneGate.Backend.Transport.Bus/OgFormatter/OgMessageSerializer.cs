using System;
using System.IO;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using MassTransit;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Bus.OgFormatter
{
    public class OgMessageSerializer : IMessageSerializer
    {
        public static ContentType ContentTypeValue { get; } = new ContentType("application/vnd.onegate+json");
        public ContentType ContentType => ContentTypeValue;

        private static readonly Lazy<JsonSerializer> LazyDeserializer;
        private static readonly Lazy<Encoding> LazyEncoding;
        private static readonly Lazy<JsonSerializer> LazySerializer;

        static OgMessageSerializer()
        {
            LazyEncoding =
                new Lazy<Encoding>(() => new UTF8Encoding(false, true), LazyThreadSafetyMode.PublicationOnly);

            var deserializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                TypeNameHandling = TypeNameHandling.None,
                DateParseHandling = DateParseHandling.None,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
            };

            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                TypeNameHandling = TypeNameHandling.None,
                DateParseHandling = DateParseHandling.None,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
            };

            LazyDeserializer = new Lazy<JsonSerializer>(() => JsonSerializer.Create(deserializerSettings));
            LazySerializer = new Lazy<JsonSerializer>(() => JsonSerializer.Create(serializerSettings));
        }

        public static JsonSerializer DeserializerInstance => LazyDeserializer.Value;
        private static JsonSerializer SerializerInstance => LazySerializer.Value;
        private static Encoding Encoding => LazyEncoding.Value;

        public void Serialize<T>(Stream stream, SendContext<T> context) where T : class
        {
            try
            {
                context.ContentType = ContentTypeValue;
                var envelope = OgEnvelope.FromSendContext(context, context.Message);

                using var writer = new StreamWriter(stream, Encoding, 1024, true);
                using var jsonWriter = new JsonTextWriter(writer)
                {
                    Formatting = Formatting.Indented
                };

                SerializerInstance.Serialize(jsonWriter, envelope, typeof(OgEnvelope));

                jsonWriter.Flush();
                writer.Flush();
            }
            catch (SerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SerializationException("Failed to serialize message", ex);
            }
        }
    }
}