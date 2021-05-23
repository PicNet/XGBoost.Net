# XGBoost.Net
.Net wrapper for [XGBoost](https://github.com/dmlc/xgboost) based off the [Python API](https://xgboost.readthedocs.io/en/latest/python/index.html).

Available as a [NuGet package](https://www.nuget.org/packages/PicNet.XGBoost/).

Notes: For tests, loading the dll doesn't seem to work when referencing as a shared project, but loading as a nuget package works. So the Tests will fail in the XGBoost solution but will work in the XGBoostTests Solution.

Did multitargeting for the library (only) based on this post:
https://weblog.west-wind.com/posts/2017/jun/22/multitargeting-and-porting-a-net-library-to-net-core-20#Running-Tests-in-Visual-Studio----One-Framework-at-a-Time