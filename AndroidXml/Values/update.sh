#!/bin/bash
url=https://android.googlesource.com/platform/frameworks/base/+/master/core/res/res/values/public.xml
curl "$url?format=TEXT"| base64 --decode > public.xml
