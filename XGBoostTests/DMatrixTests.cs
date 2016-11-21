﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using XGBoost;
using System.IO;

namespace XGBoostTests
{
  [TestClass]
  public class DMatrixTests
  {
    [TestMethod]
    public void NumCol()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      int cols = d.NumCol();
      Assert.AreEqual(cols, 127);
    }

    [TestMethod]
    public void NumRow()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      int rows = d.NumRow();
      Assert.AreEqual(rows, 1611);
    }

    [TestMethod]
    public void GetFloatInfo()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      
      float[] labelInfo = d.GetFloatInfo("label");
      float[] first5ActualLabelInfo = { 0, 1, 0, 0, 0 };
      for (int i = 0; i < first5ActualLabelInfo.Length; i++)
      {
        string errorMsg = "Labels at index " + i.ToString() + " are not equal";
        Assert.AreEqual(labelInfo[i], first5ActualLabelInfo[i], 0.01, errorMsg);
      }

      float[] weightInfo = d.GetFloatInfo("weight");
      float[] actualWeightInfo = { };
      // both should be empty
      Assert.AreEqual(weightInfo.Length, actualWeightInfo.Length); 

      float[] baseMariginInfo = d.GetFloatInfo("base_margin");
      float[] actualBaseMarginInfo = { };
      // both should be empty
      Assert.AreEqual(baseMariginInfo.Length, actualBaseMarginInfo.Length); 
    }

    [TestMethod]
    public void BaseMargin_Get()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      // both should be empty
      Assert.AreEqual(d.BaseMargin.Length, 0);
      Assert.AreEqual(d.BaseMargin.Length, d.GetFloatInfo("base_margin").Length);
    }

    [TestMethod]
    public void Label_Get()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      float[] label = d.Label;
      float[] actualLabel = d.GetFloatInfo("label");

      Assert.AreEqual(label.Length, actualLabel.Length);
      for (int i = 0; i < label.Length; i++)
      {
        string errorMsg = "Labels at index " + i.ToString() + " are not equal";
        Assert.AreEqual(label[i], actualLabel[i], 0.01);
      }
    }

    [TestMethod]
    public void Weight_Get()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      // both should be empty
      Assert.AreEqual(d.Weight.Length, 0);
      Assert.AreEqual(d.Weight.Length, d.GetFloatInfo("weight").Length);
    }

    [TestMethod]
    public void SetFloatInfo()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");

      float[] floatInfo = { 42 };
      d.SetFloatInfo("label", floatInfo);
      float[] dFloatInfo = d.GetFloatInfo("label");
      Assert.AreEqual(floatInfo[0], dFloatInfo[0]);

      d.SetFloatInfo("weight", floatInfo);
      dFloatInfo = d.GetFloatInfo("weight");
      Assert.AreEqual(floatInfo[0], dFloatInfo[0]);

      d.SetFloatInfo("base_margin", floatInfo);
      dFloatInfo = d.GetFloatInfo("base_margin");
      Assert.AreEqual(floatInfo[0], dFloatInfo[0]);
    }

    [TestMethod]
    public void BaseMargin_Set()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");

      float[] baseMargin = { 42 };
      d.BaseMargin = baseMargin;
      float[] dBaseMargin = d.BaseMargin;
      Assert.AreEqual(baseMargin[0], dBaseMargin[0]);
    }

    [TestMethod]
    public void Label_Set()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");

      float[] label = { 42 };
      d.Label = label;
      float[] dLabel = d.Label;
      Assert.AreEqual(label[0], dLabel[0]);
    }

    [TestMethod]
    public void Weight_Set()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");

      float[] weight = { 42 };
      d.Weight = weight;
      float[] dWeight = d.Weight;
      Assert.AreEqual(weight[0], dWeight[0]);
    }

    [TestMethod]
    public void SaveBinary()
    {
      DMatrix orgDMatrix = new DMatrix("libs/agaricus.txt.test");
      orgDMatrix.SaveBinary("libs/agaricus.txt.test.bin");
      DMatrix binDMatrix = new DMatrix("libs/agaricus.txt.test.bin");
      Assert.AreEqual(orgDMatrix.NumCol(), binDMatrix.NumCol());
      File.Delete("libs/agaricus.txt.test.bin");
    }

    [TestMethod]
    public void SetGroup()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      int[] group = { 1, 3, 7 };
      d.SetGroup(group);
      // if we reach here with no exception then we assume it passes
    }
  }
}
