// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Drawing.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Greenshot2.Editor.FileFormat {

  /// <summary>Holder for reflection information generated from Drawing.proto</summary>
  public static partial class DrawingReflection {

    #region Descriptor
    /// <summary>File descriptor for Drawing.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static DrawingReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1EcmF3aW5nLnByb3RvEhxncmVlbnNob3QyLmVkaXRvci5maWxlZm9ybWF0",
            "Gg9CYXNlVHlwZXMucHJvdG8aDFNoYXBlcy5wcm90byI8CgVMYXllchIzCgZz",
            "aGFwZXMYASADKAsyIy5ncmVlbnNob3QyLmVkaXRvci5maWxlZm9ybWF0LlNo",
            "YXBlIowDCgdEcmF3aW5nEg0KBXRpdGxlGAEgASgJEkcKCXJlc291cmNlcxgC",
            "IAMoCzI0LmdyZWVuc2hvdDIuZWRpdG9yLmZpbGVmb3JtYXQuRHJhd2luZy5S",
            "ZXNvdXJjZXNFbnRyeRJBCgZzdHlsZXMYAyADKAsyMS5ncmVlbnNob3QyLmVk",
            "aXRvci5maWxlZm9ybWF0LkRyYXdpbmcuU3R5bGVzRW50cnkSMwoGbGF5ZXJz",
            "GAQgAygLMiMuZ3JlZW5zaG90Mi5lZGl0b3IuZmlsZWZvcm1hdC5MYXllchpY",
            "Cg5SZXNvdXJjZXNFbnRyeRILCgNrZXkYASABKAkSNQoFdmFsdWUYAiABKAsy",
            "Ji5ncmVlbnNob3QyLmVkaXRvci5maWxlZm9ybWF0LlJlc291cmNlOgI4ARpX",
            "CgtTdHlsZXNFbnRyeRILCgNrZXkYASABKAkSNwoFdmFsdWUYAiABKAsyKC5n",
            "cmVlbnNob3QyLmVkaXRvci5maWxlZm9ybWF0LlNoYXBlU3R5bGU6AjgBQh+q",
            "AhxHcmVlbnNob3QyLkVkaXRvci5GaWxlRm9ybWF0YgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Greenshot2.Editor.FileFormat.BaseTypesReflection.Descriptor, global::Greenshot2.Editor.FileFormat.ShapesReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Greenshot2.Editor.FileFormat.Layer), global::Greenshot2.Editor.FileFormat.Layer.Parser, new[]{ "Shapes" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Greenshot2.Editor.FileFormat.Drawing), global::Greenshot2.Editor.FileFormat.Drawing.Parser, new[]{ "Title", "Resources", "Styles", "Layers" }, null, null, new pbr::GeneratedClrTypeInfo[] { null, null, })
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// This defines a layer
  /// </summary>
  public sealed partial class Layer : pb::IMessage<Layer> {
    private static readonly pb::MessageParser<Layer> _parser = new pb::MessageParser<Layer>(() => new Layer());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Layer> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Greenshot2.Editor.FileFormat.DrawingReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Layer() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Layer(Layer other) : this() {
      shapes_ = other.shapes_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Layer Clone() {
      return new Layer(this);
    }

    /// <summary>Field number for the "shapes" field.</summary>
    public const int ShapesFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Greenshot2.Editor.FileFormat.Shape> _repeated_shapes_codec
        = pb::FieldCodec.ForMessage(10, global::Greenshot2.Editor.FileFormat.Shape.Parser);
    private readonly pbc::RepeatedField<global::Greenshot2.Editor.FileFormat.Shape> shapes_ = new pbc::RepeatedField<global::Greenshot2.Editor.FileFormat.Shape>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Greenshot2.Editor.FileFormat.Shape> Shapes {
      get { return shapes_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Layer);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Layer other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!shapes_.Equals(other.shapes_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= shapes_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      shapes_.WriteTo(output, _repeated_shapes_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += shapes_.CalculateSize(_repeated_shapes_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Layer other) {
      if (other == null) {
        return;
      }
      shapes_.Add(other.shapes_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            shapes_.AddEntriesFrom(input, _repeated_shapes_codec);
            break;
          }
        }
      }
    }

  }

  /// <summary>
  /// This defines the actual file format
  /// </summary>
  public sealed partial class Drawing : pb::IMessage<Drawing> {
    private static readonly pb::MessageParser<Drawing> _parser = new pb::MessageParser<Drawing>(() => new Drawing());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Drawing> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Greenshot2.Editor.FileFormat.DrawingReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Drawing() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Drawing(Drawing other) : this() {
      title_ = other.title_;
      resources_ = other.resources_.Clone();
      styles_ = other.styles_.Clone();
      layers_ = other.layers_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Drawing Clone() {
      return new Drawing(this);
    }

    /// <summary>Field number for the "title" field.</summary>
    public const int TitleFieldNumber = 1;
    private string title_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Title {
      get { return title_; }
      set {
        title_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "resources" field.</summary>
    public const int ResourcesFieldNumber = 2;
    private static readonly pbc::MapField<string, global::Greenshot2.Editor.FileFormat.Resource>.Codec _map_resources_codec
        = new pbc::MapField<string, global::Greenshot2.Editor.FileFormat.Resource>.Codec(pb::FieldCodec.ForString(10), pb::FieldCodec.ForMessage(18, global::Greenshot2.Editor.FileFormat.Resource.Parser), 18);
    private readonly pbc::MapField<string, global::Greenshot2.Editor.FileFormat.Resource> resources_ = new pbc::MapField<string, global::Greenshot2.Editor.FileFormat.Resource>();
    /// <summary>
    /// Resources are stored with a string GUID as reference
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<string, global::Greenshot2.Editor.FileFormat.Resource> Resources {
      get { return resources_; }
    }

    /// <summary>Field number for the "styles" field.</summary>
    public const int StylesFieldNumber = 3;
    private static readonly pbc::MapField<string, global::Greenshot2.Editor.FileFormat.ShapeStyle>.Codec _map_styles_codec
        = new pbc::MapField<string, global::Greenshot2.Editor.FileFormat.ShapeStyle>.Codec(pb::FieldCodec.ForString(10), pb::FieldCodec.ForMessage(18, global::Greenshot2.Editor.FileFormat.ShapeStyle.Parser), 26);
    private readonly pbc::MapField<string, global::Greenshot2.Editor.FileFormat.ShapeStyle> styles_ = new pbc::MapField<string, global::Greenshot2.Editor.FileFormat.ShapeStyle>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<string, global::Greenshot2.Editor.FileFormat.ShapeStyle> Styles {
      get { return styles_; }
    }

    /// <summary>Field number for the "layers" field.</summary>
    public const int LayersFieldNumber = 4;
    private static readonly pb::FieldCodec<global::Greenshot2.Editor.FileFormat.Layer> _repeated_layers_codec
        = pb::FieldCodec.ForMessage(34, global::Greenshot2.Editor.FileFormat.Layer.Parser);
    private readonly pbc::RepeatedField<global::Greenshot2.Editor.FileFormat.Layer> layers_ = new pbc::RepeatedField<global::Greenshot2.Editor.FileFormat.Layer>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Greenshot2.Editor.FileFormat.Layer> Layers {
      get { return layers_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Drawing);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Drawing other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Title != other.Title) return false;
      if (!Resources.Equals(other.Resources)) return false;
      if (!Styles.Equals(other.Styles)) return false;
      if(!layers_.Equals(other.layers_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Title.Length != 0) hash ^= Title.GetHashCode();
      hash ^= Resources.GetHashCode();
      hash ^= Styles.GetHashCode();
      hash ^= layers_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Title.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Title);
      }
      resources_.WriteTo(output, _map_resources_codec);
      styles_.WriteTo(output, _map_styles_codec);
      layers_.WriteTo(output, _repeated_layers_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Title.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Title);
      }
      size += resources_.CalculateSize(_map_resources_codec);
      size += styles_.CalculateSize(_map_styles_codec);
      size += layers_.CalculateSize(_repeated_layers_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Drawing other) {
      if (other == null) {
        return;
      }
      if (other.Title.Length != 0) {
        Title = other.Title;
      }
      resources_.Add(other.resources_);
      styles_.Add(other.styles_);
      layers_.Add(other.layers_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Title = input.ReadString();
            break;
          }
          case 18: {
            resources_.AddEntriesFrom(input, _map_resources_codec);
            break;
          }
          case 26: {
            styles_.AddEntriesFrom(input, _map_styles_codec);
            break;
          }
          case 34: {
            layers_.AddEntriesFrom(input, _repeated_layers_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
