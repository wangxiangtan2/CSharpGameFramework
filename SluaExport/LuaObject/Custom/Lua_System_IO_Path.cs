﻿using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_System_IO_Path : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ChangeExtension_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			var ret=System.IO.Path.ChangeExtension(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Combine_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			var ret=System.IO.Path.Combine(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetDirectoryName_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=System.IO.Path.GetDirectoryName(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetExtension_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=System.IO.Path.GetExtension(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetFileName_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=System.IO.Path.GetFileName(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetFileNameWithoutExtension_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=System.IO.Path.GetFileNameWithoutExtension(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetFullPath_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=System.IO.Path.GetFullPath(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetPathRoot_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=System.IO.Path.GetPathRoot(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetTempFileName_s(IntPtr l) {
		try {
			var ret=System.IO.Path.GetTempFileName();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetTempPath_s(IntPtr l) {
		try {
			var ret=System.IO.Path.GetTempPath();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int HasExtension_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=System.IO.Path.HasExtension(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int IsPathRooted_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=System.IO.Path.IsPathRooted(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetInvalidFileNameChars_s(IntPtr l) {
		try {
			var ret=System.IO.Path.GetInvalidFileNameChars();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetInvalidPathChars_s(IntPtr l) {
		try {
			var ret=System.IO.Path.GetInvalidPathChars();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetRandomFileName_s(IntPtr l) {
		try {
			var ret=System.IO.Path.GetRandomFileName();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_AltDirectorySeparatorChar(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.IO.Path.AltDirectorySeparatorChar);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_DirectorySeparatorChar(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.IO.Path.DirectorySeparatorChar);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_PathSeparator(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.IO.Path.PathSeparator);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_VolumeSeparatorChar(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.IO.Path.VolumeSeparatorChar);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"System.IO.Path");
		addMember(l,ChangeExtension_s);
		addMember(l,Combine_s);
		addMember(l,GetDirectoryName_s);
		addMember(l,GetExtension_s);
		addMember(l,GetFileName_s);
		addMember(l,GetFileNameWithoutExtension_s);
		addMember(l,GetFullPath_s);
		addMember(l,GetPathRoot_s);
		addMember(l,GetTempFileName_s);
		addMember(l,GetTempPath_s);
		addMember(l,HasExtension_s);
		addMember(l,IsPathRooted_s);
		addMember(l,GetInvalidFileNameChars_s);
		addMember(l,GetInvalidPathChars_s);
		addMember(l,GetRandomFileName_s);
		addMember(l,"AltDirectorySeparatorChar",get_AltDirectorySeparatorChar,null,false);
		addMember(l,"DirectorySeparatorChar",get_DirectorySeparatorChar,null,false);
		addMember(l,"PathSeparator",get_PathSeparator,null,false);
		addMember(l,"VolumeSeparatorChar",get_VolumeSeparatorChar,null,false);
		createTypeMetatable(l,null, typeof(System.IO.Path));
	}
}
