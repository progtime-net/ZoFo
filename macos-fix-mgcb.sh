#!/usr/bin/env bash

brew install freetype
brew install freeimage
mgcb_ver=$(ls ~/.nuget/packages/dotnet-mgcb/)
mgcb_path=~/.nuget/packages/dotnet-mgcb/$mgcb_ver/tools/net6.0/any/

rm $mgcb_path/libfreetype6.dylib $mgcb_path/libFreeImage.dylib

ln -s /opt/homebrew/lib/libfreetype.dylib $mgcb_path/libfreetype6.dylib
ln -s /opt/homebrew/lib/libfreeimage.dylib $mgcb_path/libFreeImage.dylib
