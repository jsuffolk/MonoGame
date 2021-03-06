#region License
/*
Microsoft Public License (Ms-PL)
MonoGame - Copyright © 2009 The MonoGame Team

All rights reserved.

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not
accept the license, do not use the software.

1. Definitions
The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under 
U.S. copyright law.

A "contribution" is the original software, or any additions or changes to the software.
A "contributor" is any person that distributes its contribution under this license.
"Licensed patents" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations
(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
your patent license from such contributor to the software ends automatically.
(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
notices that are present in the software.
(D) If you distribute any portion of the software in source code form, you may do so only under this license by including 
a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object 
code form, you may only do so under a license that complies with this license.
(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees
or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent
permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular
purpose and non-infringement.
*/
#endregion License

using System;
using System.Collections.Generic;
#if WINRT
using System.Reflection;
#endif
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Microsoft.Xna.Framework.Content
{
	internal class EffectMaterialReader : ContentTypeReader<EffectMaterial>
	{
		protected internal override EffectMaterial Read (ContentReader input, EffectMaterial existingInstance)
		{
			var effect = input.ReadExternalReference<Effect> ();
			var effectMaterial = new EffectMaterial (effect);

			var dict = input.ReadObject<Dictionary<string, object>> ();

			foreach (KeyValuePair<string, object> item in dict) {
				var parameter = effectMaterial.Parameters [item.Key];
				if (parameter != null) {
#if WINRT
					if (typeof(Texture).GetTypeInfo().IsAssignableFrom(item.Value.GetType().GetTypeInfo())){
#else
                    if (typeof(Texture).IsAssignableFrom(item.Value.GetType()))
                    {
#endif
                        parameter.SetValue((Texture)item.Value);
                    }
#if WINRT
					else if (typeof(Vector2).GetTypeInfo().IsAssignableFrom(item.Value.GetType().GetTypeInfo())){
#else
                    else if (typeof(Vector2).IsAssignableFrom(item.Value.GetType()))
                    {
#endif
                        parameter.SetValue((Vector2)item.Value);
                    }
#if WINRT
					else if (typeof(Vector3).GetTypeInfo().IsAssignableFrom(item.Value.GetType().GetTypeInfo())){
#else
                    else if (typeof(Vector3).IsAssignableFrom(item.Value.GetType()))
                    {
#endif
                        parameter.SetValue((Vector3)item.Value);
                    }
#if WINRT
					else if (typeof(Vector4).GetTypeInfo().IsAssignableFrom(item.Value.GetType().GetTypeInfo())){
#else
                    else if (typeof(Vector4).IsAssignableFrom(item.Value.GetType()))
                    {
#endif
                        parameter.SetValue((Vector4)item.Value);
                    }
#if WINRT
					else if (typeof(Single).GetTypeInfo().IsAssignableFrom(item.Value.GetType().GetTypeInfo())){
#else
                    else if (typeof(Single).IsAssignableFrom(item.Value.GetType()))
                    {
#endif
                        parameter.SetValue((Single)item.Value);
                    }
#if WINRT
					else if (typeof(int).GetTypeInfo().IsAssignableFrom(item.Value.GetType().GetTypeInfo())){
#else
                    else if (typeof(int).IsAssignableFrom(item.Value.GetType()))
                    {
#endif
                        parameter.SetValue((int)item.Value);
                    }
#if WINRT
					else if (typeof(long).GetTypeInfo().IsAssignableFrom(item.Value.GetType().GetTypeInfo())){
#else
                    else if (typeof(long).IsAssignableFrom(item.Value.GetType()))
                    {
#endif
                        parameter.SetValue((long)item.Value);
                    }
                    else
                    {
                        throw new Exception(
                            string.Format(
                            "EffectMaterialReader Failure '{0}' on Type: '{1}", item.Key, item.Value.GetType()
                            ));
                    }
				} else {
					Debug.WriteLine ("No parameter " + item.Key);
				}
			}


			return effectMaterial;
		}
	}
}
