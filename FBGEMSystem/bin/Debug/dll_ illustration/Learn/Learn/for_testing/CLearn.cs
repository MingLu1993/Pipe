/*
* MATLAB Compiler: 6.0 (R2015a)
* Date: Fri Mar 03 11:55:32 2017
* Arguments: "-B" "macro_default" "-W" "dotnet:Learn,CLearn,0.0,private" "-T" "link:lib"
* "-d" "G:\pipe\Pipe\FBGEMSystem\bin\Debug\dll_ illustration\Learn\Learn\for_testing"
* "-v" "class{CLearn:G:\pipe\Pipe\FBGEMSystem\bin\Debug\dll_
* illustration\Learn\Predict.m,G:\pipe\Pipe\FBGEMSystem\bin\Debug\dll_
* illustration\Learn\PredictMacro.m,G:\pipe\Pipe\FBGEMSystem\bin\Debug\dll_
* illustration\Learn\train.m,G:\pipe\Pipe\FBGEMSystem\bin\Debug\dll_
* illustration\Learn\trainMacro.m}" 
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace Learn
{

  /// <summary>
  /// The CLearn class provides a CLS compliant, MWArray interface to the MATLAB
  /// functions contained in the files:
  /// <newpara></newpara>
  /// G:\pipe\Pipe\FBGEMSystem\bin\Debug\dll_ illustration\Learn\Predict.m
  /// <newpara></newpara>
  /// G:\pipe\Pipe\FBGEMSystem\bin\Debug\dll_ illustration\Learn\PredictMacro.m
  /// <newpara></newpara>
  /// G:\pipe\Pipe\FBGEMSystem\bin\Debug\dll_ illustration\Learn\train.m
  /// <newpara></newpara>
  /// G:\pipe\Pipe\FBGEMSystem\bin\Debug\dll_ illustration\Learn\trainMacro.m
  /// </summary>
  /// <remarks>
  /// @Version 0.0
  /// </remarks>
  public class CLearn : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB runtime instance.
    /// </summary>
    static CLearn()
    {
      if (MWMCR.MCRAppInitialized)
      {
        try
        {
          Assembly assembly= Assembly.GetExecutingAssembly();

          string ctfFilePath= assembly.Location;

          int lastDelimiter= ctfFilePath.LastIndexOf(@"\");

          ctfFilePath= ctfFilePath.Remove(lastDelimiter, (ctfFilePath.Length - lastDelimiter));

          string ctfFileName = "Learn.ctf";

          Stream embeddedCtfStream = null;

          String[] resourceStrings = assembly.GetManifestResourceNames();

          foreach (String name in resourceStrings)
          {
            if (name.Contains(ctfFileName))
            {
              embeddedCtfStream = assembly.GetManifestResourceStream(name);
              break;
            }
          }
          mcr= new MWMCR("",
                         ctfFilePath, embeddedCtfStream, true);
        }
        catch(Exception ex)
        {
          ex_ = new Exception("MWArray assembly failed to be initialized", ex);
        }
      }
      else
      {
        ex_ = new ApplicationException("MWArray assembly could not be initialized");
      }
    }


    /// <summary>
    /// Constructs a new instance of the CLearn class.
    /// </summary>
    public CLearn()
    {
      if(ex_ != null)
      {
        throw ex_;
      }
    }


    #endregion Constructors

    #region Finalize

    /// <summary internal= "true">
    /// Class destructor called by the CLR garbage collector.
    /// </summary>
    ~CLearn()
    {
      Dispose(false);
    }


    /// <summary>
    /// Frees the native resources associated with this object
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }


    /// <summary internal= "true">
    /// Internal dispose function
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposed)
      {
        disposed= true;

        if (disposing)
        {
          // Free managed resources;
        }

        // Free native resources
      }
    }


    #endregion Finalize

    #region Methods

    /// <summary>
    /// Provides a single output, 0-input MWArrayinterface to the Predict MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray Predict()
    {
      return mcr.EvaluateFunction("Predict", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the Predict MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray Predict(MWArray k)
    {
      return mcr.EvaluateFunction("Predict", k);
    }


    /// <summary>
    /// Provides a single output, 2-input MWArrayinterface to the Predict MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray Predict(MWArray k, MWArray T)
    {
      return mcr.EvaluateFunction("Predict", k, T);
    }


    /// <summary>
    /// Provides a single output, 3-input MWArrayinterface to the Predict MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray Predict(MWArray k, MWArray T, MWArray FinalTree)
    {
      return mcr.EvaluateFunction("Predict", k, T, FinalTree);
    }


    /// <summary>
    /// Provides a single output, 4-input MWArrayinterface to the Predict MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray Predict(MWArray k, MWArray T, MWArray FinalTree, MWArray test_patterns)
    {
      return mcr.EvaluateFunction("Predict", k, T, FinalTree, test_patterns);
    }


    /// <summary>
    /// Provides a single output, 5-input MWArrayinterface to the Predict MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="FinalBeta">Input argument #5</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray Predict(MWArray k, MWArray T, MWArray FinalTree, MWArray 
                     test_patterns, MWArray FinalBeta)
    {
      return mcr.EvaluateFunction("Predict", k, T, FinalTree, test_patterns, FinalBeta);
    }


    /// <summary>
    /// Provides a single output, 6-input MWArrayinterface to the Predict MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="FinalBeta">Input argument #5</param>
    /// <param name="FinalEnsembleBeta">Input argument #6</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray Predict(MWArray k, MWArray T, MWArray FinalTree, MWArray 
                     test_patterns, MWArray FinalBeta, MWArray FinalEnsembleBeta)
    {
      return mcr.EvaluateFunction("Predict", k, T, FinalTree, test_patterns, FinalBeta, FinalEnsembleBeta);
    }


    /// <summary>
    /// Provides a single output, 7-input MWArrayinterface to the Predict MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="FinalBeta">Input argument #5</param>
    /// <param name="FinalEnsembleBeta">Input argument #6</param>
    /// <param name="discrete_dim">Input argument #7</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray Predict(MWArray k, MWArray T, MWArray FinalTree, MWArray 
                     test_patterns, MWArray FinalBeta, MWArray FinalEnsembleBeta, MWArray 
                     discrete_dim)
    {
      return mcr.EvaluateFunction("Predict", k, T, FinalTree, test_patterns, FinalBeta, FinalEnsembleBeta, discrete_dim);
    }


    /// <summary>
    /// Provides a single output, 8-input MWArrayinterface to the Predict MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="FinalBeta">Input argument #5</param>
    /// <param name="FinalEnsembleBeta">Input argument #6</param>
    /// <param name="discrete_dim">Input argument #7</param>
    /// <param name="TargetsNum">Input argument #8</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray Predict(MWArray k, MWArray T, MWArray FinalTree, MWArray 
                     test_patterns, MWArray FinalBeta, MWArray FinalEnsembleBeta, MWArray 
                     discrete_dim, MWArray TargetsNum)
    {
      return mcr.EvaluateFunction("Predict", k, T, FinalTree, test_patterns, FinalBeta, FinalEnsembleBeta, discrete_dim, TargetsNum);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the Predict MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] Predict(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "Predict", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the Predict MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] Predict(int numArgsOut, MWArray k)
    {
      return mcr.EvaluateFunction(numArgsOut, "Predict", k);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the Predict MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] Predict(int numArgsOut, MWArray k, MWArray T)
    {
      return mcr.EvaluateFunction(numArgsOut, "Predict", k, T);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the Predict MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] Predict(int numArgsOut, MWArray k, MWArray T, MWArray FinalTree)
    {
      return mcr.EvaluateFunction(numArgsOut, "Predict", k, T, FinalTree);
    }


    /// <summary>
    /// Provides the standard 4-input MWArray interface to the Predict MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] Predict(int numArgsOut, MWArray k, MWArray T, MWArray FinalTree, 
                       MWArray test_patterns)
    {
      return mcr.EvaluateFunction(numArgsOut, "Predict", k, T, FinalTree, test_patterns);
    }


    /// <summary>
    /// Provides the standard 5-input MWArray interface to the Predict MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="FinalBeta">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] Predict(int numArgsOut, MWArray k, MWArray T, MWArray FinalTree, 
                       MWArray test_patterns, MWArray FinalBeta)
    {
      return mcr.EvaluateFunction(numArgsOut, "Predict", k, T, FinalTree, test_patterns, FinalBeta);
    }


    /// <summary>
    /// Provides the standard 6-input MWArray interface to the Predict MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="FinalBeta">Input argument #5</param>
    /// <param name="FinalEnsembleBeta">Input argument #6</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] Predict(int numArgsOut, MWArray k, MWArray T, MWArray FinalTree, 
                       MWArray test_patterns, MWArray FinalBeta, MWArray 
                       FinalEnsembleBeta)
    {
      return mcr.EvaluateFunction(numArgsOut, "Predict", k, T, FinalTree, test_patterns, FinalBeta, FinalEnsembleBeta);
    }


    /// <summary>
    /// Provides the standard 7-input MWArray interface to the Predict MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="FinalBeta">Input argument #5</param>
    /// <param name="FinalEnsembleBeta">Input argument #6</param>
    /// <param name="discrete_dim">Input argument #7</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] Predict(int numArgsOut, MWArray k, MWArray T, MWArray FinalTree, 
                       MWArray test_patterns, MWArray FinalBeta, MWArray 
                       FinalEnsembleBeta, MWArray discrete_dim)
    {
      return mcr.EvaluateFunction(numArgsOut, "Predict", k, T, FinalTree, test_patterns, FinalBeta, FinalEnsembleBeta, discrete_dim);
    }


    /// <summary>
    /// Provides the standard 8-input MWArray interface to the Predict MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="FinalBeta">Input argument #5</param>
    /// <param name="FinalEnsembleBeta">Input argument #6</param>
    /// <param name="discrete_dim">Input argument #7</param>
    /// <param name="TargetsNum">Input argument #8</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] Predict(int numArgsOut, MWArray k, MWArray T, MWArray FinalTree, 
                       MWArray test_patterns, MWArray FinalBeta, MWArray 
                       FinalEnsembleBeta, MWArray discrete_dim, MWArray TargetsNum)
    {
      return mcr.EvaluateFunction(numArgsOut, "Predict", k, T, FinalTree, test_patterns, FinalBeta, FinalEnsembleBeta, discrete_dim, TargetsNum);
    }


    /// <summary>
    /// Provides an interface for the Predict function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void Predict(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("Predict", numArgsOut, ref argsOut, argsIn);
    }


    /// <summary>
    /// Provides a single output, 0-input MWArrayinterface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PredictMacro()
    {
      return mcr.EvaluateFunction("PredictMacro", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PredictMacro(MWArray k)
    {
      return mcr.EvaluateFunction("PredictMacro", k);
    }


    /// <summary>
    /// Provides a single output, 2-input MWArrayinterface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PredictMacro(MWArray k, MWArray T)
    {
      return mcr.EvaluateFunction("PredictMacro", k, T);
    }


    /// <summary>
    /// Provides a single output, 3-input MWArrayinterface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PredictMacro(MWArray k, MWArray T, MWArray FinalTree)
    {
      return mcr.EvaluateFunction("PredictMacro", k, T, FinalTree);
    }


    /// <summary>
    /// Provides a single output, 4-input MWArrayinterface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PredictMacro(MWArray k, MWArray T, MWArray FinalTree, MWArray 
                          test_patterns)
    {
      return mcr.EvaluateFunction("PredictMacro", k, T, FinalTree, test_patterns);
    }


    /// <summary>
    /// Provides a single output, 5-input MWArrayinterface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="Finalmecro">Input argument #5</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PredictMacro(MWArray k, MWArray T, MWArray FinalTree, MWArray 
                          test_patterns, MWArray Finalmecro)
    {
      return mcr.EvaluateFunction("PredictMacro", k, T, FinalTree, test_patterns, Finalmecro);
    }


    /// <summary>
    /// Provides a single output, 6-input MWArrayinterface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="Finalmecro">Input argument #5</param>
    /// <param name="FinalMECRO">Input argument #6</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PredictMacro(MWArray k, MWArray T, MWArray FinalTree, MWArray 
                          test_patterns, MWArray Finalmecro, MWArray FinalMECRO)
    {
      return mcr.EvaluateFunction("PredictMacro", k, T, FinalTree, test_patterns, Finalmecro, FinalMECRO);
    }


    /// <summary>
    /// Provides a single output, 7-input MWArrayinterface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="Finalmecro">Input argument #5</param>
    /// <param name="FinalMECRO">Input argument #6</param>
    /// <param name="discrete_dim">Input argument #7</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PredictMacro(MWArray k, MWArray T, MWArray FinalTree, MWArray 
                          test_patterns, MWArray Finalmecro, MWArray FinalMECRO, MWArray 
                          discrete_dim)
    {
      return mcr.EvaluateFunction("PredictMacro", k, T, FinalTree, test_patterns, Finalmecro, FinalMECRO, discrete_dim);
    }


    /// <summary>
    /// Provides a single output, 8-input MWArrayinterface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="Finalmecro">Input argument #5</param>
    /// <param name="FinalMECRO">Input argument #6</param>
    /// <param name="discrete_dim">Input argument #7</param>
    /// <param name="TargetsNum">Input argument #8</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray PredictMacro(MWArray k, MWArray T, MWArray FinalTree, MWArray 
                          test_patterns, MWArray Finalmecro, MWArray FinalMECRO, MWArray 
                          discrete_dim, MWArray TargetsNum)
    {
      return mcr.EvaluateFunction("PredictMacro", k, T, FinalTree, test_patterns, Finalmecro, FinalMECRO, discrete_dim, TargetsNum);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PredictMacro(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "PredictMacro", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PredictMacro(int numArgsOut, MWArray k)
    {
      return mcr.EvaluateFunction(numArgsOut, "PredictMacro", k);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PredictMacro(int numArgsOut, MWArray k, MWArray T)
    {
      return mcr.EvaluateFunction(numArgsOut, "PredictMacro", k, T);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PredictMacro(int numArgsOut, MWArray k, MWArray T, MWArray FinalTree)
    {
      return mcr.EvaluateFunction(numArgsOut, "PredictMacro", k, T, FinalTree);
    }


    /// <summary>
    /// Provides the standard 4-input MWArray interface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PredictMacro(int numArgsOut, MWArray k, MWArray T, MWArray 
                            FinalTree, MWArray test_patterns)
    {
      return mcr.EvaluateFunction(numArgsOut, "PredictMacro", k, T, FinalTree, test_patterns);
    }


    /// <summary>
    /// Provides the standard 5-input MWArray interface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="Finalmecro">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PredictMacro(int numArgsOut, MWArray k, MWArray T, MWArray 
                            FinalTree, MWArray test_patterns, MWArray Finalmecro)
    {
      return mcr.EvaluateFunction(numArgsOut, "PredictMacro", k, T, FinalTree, test_patterns, Finalmecro);
    }


    /// <summary>
    /// Provides the standard 6-input MWArray interface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="Finalmecro">Input argument #5</param>
    /// <param name="FinalMECRO">Input argument #6</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PredictMacro(int numArgsOut, MWArray k, MWArray T, MWArray 
                            FinalTree, MWArray test_patterns, MWArray Finalmecro, MWArray 
                            FinalMECRO)
    {
      return mcr.EvaluateFunction(numArgsOut, "PredictMacro", k, T, FinalTree, test_patterns, Finalmecro, FinalMECRO);
    }


    /// <summary>
    /// Provides the standard 7-input MWArray interface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="Finalmecro">Input argument #5</param>
    /// <param name="FinalMECRO">Input argument #6</param>
    /// <param name="discrete_dim">Input argument #7</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PredictMacro(int numArgsOut, MWArray k, MWArray T, MWArray 
                            FinalTree, MWArray test_patterns, MWArray Finalmecro, MWArray 
                            FinalMECRO, MWArray discrete_dim)
    {
      return mcr.EvaluateFunction(numArgsOut, "PredictMacro", k, T, FinalTree, test_patterns, Finalmecro, FinalMECRO, discrete_dim);
    }


    /// <summary>
    /// Provides the standard 8-input MWArray interface to the PredictMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="k">Input argument #1</param>
    /// <param name="T">Input argument #2</param>
    /// <param name="FinalTree">Input argument #3</param>
    /// <param name="test_patterns">Input argument #4</param>
    /// <param name="Finalmecro">Input argument #5</param>
    /// <param name="FinalMECRO">Input argument #6</param>
    /// <param name="discrete_dim">Input argument #7</param>
    /// <param name="TargetsNum">Input argument #8</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] PredictMacro(int numArgsOut, MWArray k, MWArray T, MWArray 
                            FinalTree, MWArray test_patterns, MWArray Finalmecro, MWArray 
                            FinalMECRO, MWArray discrete_dim, MWArray TargetsNum)
    {
      return mcr.EvaluateFunction(numArgsOut, "PredictMacro", k, T, FinalTree, test_patterns, Finalmecro, FinalMECRO, discrete_dim, TargetsNum);
    }


    /// <summary>
    /// Provides an interface for the PredictMacro function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// UNTITLED5 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void PredictMacro(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("PredictMacro", numArgsOut, ref argsOut, argsIn);
    }


    /// <summary>
    /// Provides a single output, 0-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train()
    {
      return mcr.EvaluateFunction("train", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train(MWArray FinalTree_in1)
    {
      return mcr.EvaluateFunction("train", FinalTree_in1);
    }


    /// <summary>
    /// Provides a single output, 2-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train(MWArray FinalTree_in1, MWArray FinalBeta_in1)
    {
      return mcr.EvaluateFunction("train", FinalTree_in1, FinalBeta_in1);
    }


    /// <summary>
    /// Provides a single output, 3-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train(MWArray FinalTree_in1, MWArray FinalBeta_in1, MWArray 
                   FinalEnsembleBeta_in1)
    {
      return mcr.EvaluateFunction("train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1);
    }


    /// <summary>
    /// Provides a single output, 4-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train(MWArray FinalTree_in1, MWArray FinalBeta_in1, MWArray 
                   FinalEnsembleBeta_in1, MWArray k)
    {
      return mcr.EvaluateFunction("train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k);
    }


    /// <summary>
    /// Provides a single output, 5-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train(MWArray FinalTree_in1, MWArray FinalBeta_in1, MWArray 
                   FinalEnsembleBeta_in1, MWArray k, MWArray Trains)
    {
      return mcr.EvaluateFunction("train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains);
    }


    /// <summary>
    /// Provides a single output, 6-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train(MWArray FinalTree_in1, MWArray FinalBeta_in1, MWArray 
                   FinalEnsembleBeta_in1, MWArray k, MWArray Trains, MWArray T)
    {
      return mcr.EvaluateFunction("train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains, T);
    }


    /// <summary>
    /// Provides a single output, 7-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train(MWArray FinalTree_in1, MWArray FinalBeta_in1, MWArray 
                   FinalEnsembleBeta_in1, MWArray k, MWArray Trains, MWArray T, MWArray 
                   rata)
    {
      return mcr.EvaluateFunction("train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains, T, rata);
    }


    /// <summary>
    /// Provides a single output, 8-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <param name="inc_node">Input argument #8</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train(MWArray FinalTree_in1, MWArray FinalBeta_in1, MWArray 
                   FinalEnsembleBeta_in1, MWArray k, MWArray Trains, MWArray T, MWArray 
                   rata, MWArray inc_node)
    {
      return mcr.EvaluateFunction("train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains, T, rata, inc_node);
    }


    /// <summary>
    /// Provides a single output, 9-input MWArrayinterface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <param name="inc_node">Input argument #8</param>
    /// <param name="discrete_dim">Input argument #9</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray train(MWArray FinalTree_in1, MWArray FinalBeta_in1, MWArray 
                   FinalEnsembleBeta_in1, MWArray k, MWArray Trains, MWArray T, MWArray 
                   rata, MWArray inc_node, MWArray discrete_dim)
    {
      return mcr.EvaluateFunction("train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains, T, rata, inc_node, discrete_dim);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut, MWArray FinalTree_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", FinalTree_in1);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut, MWArray FinalTree_in1, MWArray FinalBeta_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", FinalTree_in1, FinalBeta_in1);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut, MWArray FinalTree_in1, MWArray FinalBeta_in1, 
                     MWArray FinalEnsembleBeta_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1);
    }


    /// <summary>
    /// Provides the standard 4-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut, MWArray FinalTree_in1, MWArray FinalBeta_in1, 
                     MWArray FinalEnsembleBeta_in1, MWArray k)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k);
    }


    /// <summary>
    /// Provides the standard 5-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut, MWArray FinalTree_in1, MWArray FinalBeta_in1, 
                     MWArray FinalEnsembleBeta_in1, MWArray k, MWArray Trains)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains);
    }


    /// <summary>
    /// Provides the standard 6-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut, MWArray FinalTree_in1, MWArray FinalBeta_in1, 
                     MWArray FinalEnsembleBeta_in1, MWArray k, MWArray Trains, MWArray T)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains, T);
    }


    /// <summary>
    /// Provides the standard 7-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut, MWArray FinalTree_in1, MWArray FinalBeta_in1, 
                     MWArray FinalEnsembleBeta_in1, MWArray k, MWArray Trains, MWArray T, 
                     MWArray rata)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains, T, rata);
    }


    /// <summary>
    /// Provides the standard 8-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <param name="inc_node">Input argument #8</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut, MWArray FinalTree_in1, MWArray FinalBeta_in1, 
                     MWArray FinalEnsembleBeta_in1, MWArray k, MWArray Trains, MWArray T, 
                     MWArray rata, MWArray inc_node)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains, T, rata, inc_node);
    }


    /// <summary>
    /// Provides the standard 9-input MWArray interface to the train MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="FinalBeta_in1">Input argument #2</param>
    /// <param name="FinalEnsembleBeta_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <param name="inc_node">Input argument #8</param>
    /// <param name="discrete_dim">Input argument #9</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] train(int numArgsOut, MWArray FinalTree_in1, MWArray FinalBeta_in1, 
                     MWArray FinalEnsembleBeta_in1, MWArray k, MWArray Trains, MWArray T, 
                     MWArray rata, MWArray inc_node, MWArray discrete_dim)
    {
      return mcr.EvaluateFunction(numArgsOut, "train", FinalTree_in1, FinalBeta_in1, FinalEnsembleBeta_in1, k, Trains, T, rata, inc_node, discrete_dim);
    }


    /// <summary>
    /// Provides an interface for the train function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void train(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("train", numArgsOut, ref argsOut, argsIn);
    }


    /// <summary>
    /// Provides a single output, 0-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro()
    {
      return mcr.EvaluateFunction("trainMacro", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro(MWArray FinalTree_in1)
    {
      return mcr.EvaluateFunction("trainMacro", FinalTree_in1);
    }


    /// <summary>
    /// Provides a single output, 2-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro(MWArray FinalTree_in1, MWArray Finalmecro_in1)
    {
      return mcr.EvaluateFunction("trainMacro", FinalTree_in1, Finalmecro_in1);
    }


    /// <summary>
    /// Provides a single output, 3-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro(MWArray FinalTree_in1, MWArray Finalmecro_in1, MWArray 
                        FinalMECRO_in1)
    {
      return mcr.EvaluateFunction("trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1);
    }


    /// <summary>
    /// Provides a single output, 4-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro(MWArray FinalTree_in1, MWArray Finalmecro_in1, MWArray 
                        FinalMECRO_in1, MWArray k)
    {
      return mcr.EvaluateFunction("trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k);
    }


    /// <summary>
    /// Provides a single output, 5-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro(MWArray FinalTree_in1, MWArray Finalmecro_in1, MWArray 
                        FinalMECRO_in1, MWArray k, MWArray Trains)
    {
      return mcr.EvaluateFunction("trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains);
    }


    /// <summary>
    /// Provides a single output, 6-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro(MWArray FinalTree_in1, MWArray Finalmecro_in1, MWArray 
                        FinalMECRO_in1, MWArray k, MWArray Trains, MWArray T)
    {
      return mcr.EvaluateFunction("trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains, T);
    }


    /// <summary>
    /// Provides a single output, 7-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro(MWArray FinalTree_in1, MWArray Finalmecro_in1, MWArray 
                        FinalMECRO_in1, MWArray k, MWArray Trains, MWArray T, MWArray 
                        rata)
    {
      return mcr.EvaluateFunction("trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains, T, rata);
    }


    /// <summary>
    /// Provides a single output, 8-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <param name="inc_node">Input argument #8</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro(MWArray FinalTree_in1, MWArray Finalmecro_in1, MWArray 
                        FinalMECRO_in1, MWArray k, MWArray Trains, MWArray T, MWArray 
                        rata, MWArray inc_node)
    {
      return mcr.EvaluateFunction("trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains, T, rata, inc_node);
    }


    /// <summary>
    /// Provides a single output, 9-input MWArrayinterface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <param name="inc_node">Input argument #8</param>
    /// <param name="discrete_dim">Input argument #9</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray trainMacro(MWArray FinalTree_in1, MWArray Finalmecro_in1, MWArray 
                        FinalMECRO_in1, MWArray k, MWArray Trains, MWArray T, MWArray 
                        rata, MWArray inc_node, MWArray discrete_dim)
    {
      return mcr.EvaluateFunction("trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains, T, rata, inc_node, discrete_dim);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut, MWArray FinalTree_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", FinalTree_in1);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut, MWArray FinalTree_in1, MWArray 
                          Finalmecro_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", FinalTree_in1, Finalmecro_in1);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut, MWArray FinalTree_in1, MWArray 
                          Finalmecro_in1, MWArray FinalMECRO_in1)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1);
    }


    /// <summary>
    /// Provides the standard 4-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut, MWArray FinalTree_in1, MWArray 
                          Finalmecro_in1, MWArray FinalMECRO_in1, MWArray k)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k);
    }


    /// <summary>
    /// Provides the standard 5-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut, MWArray FinalTree_in1, MWArray 
                          Finalmecro_in1, MWArray FinalMECRO_in1, MWArray k, MWArray 
                          Trains)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains);
    }


    /// <summary>
    /// Provides the standard 6-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut, MWArray FinalTree_in1, MWArray 
                          Finalmecro_in1, MWArray FinalMECRO_in1, MWArray k, MWArray 
                          Trains, MWArray T)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains, T);
    }


    /// <summary>
    /// Provides the standard 7-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut, MWArray FinalTree_in1, MWArray 
                          Finalmecro_in1, MWArray FinalMECRO_in1, MWArray k, MWArray 
                          Trains, MWArray T, MWArray rata)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains, T, rata);
    }


    /// <summary>
    /// Provides the standard 8-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <param name="inc_node">Input argument #8</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut, MWArray FinalTree_in1, MWArray 
                          Finalmecro_in1, MWArray FinalMECRO_in1, MWArray k, MWArray 
                          Trains, MWArray T, MWArray rata, MWArray inc_node)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains, T, rata, inc_node);
    }


    /// <summary>
    /// Provides the standard 9-input MWArray interface to the trainMacro MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="FinalTree_in1">Input argument #1</param>
    /// <param name="Finalmecro_in1">Input argument #2</param>
    /// <param name="FinalMECRO_in1">Input argument #3</param>
    /// <param name="k">Input argument #4</param>
    /// <param name="Trains">Input argument #5</param>
    /// <param name="T">Input argument #6</param>
    /// <param name="rata">Input argument #7</param>
    /// <param name="inc_node">Input argument #8</param>
    /// <param name="discrete_dim">Input argument #9</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] trainMacro(int numArgsOut, MWArray FinalTree_in1, MWArray 
                          Finalmecro_in1, MWArray FinalMECRO_in1, MWArray k, MWArray 
                          Trains, MWArray T, MWArray rata, MWArray inc_node, MWArray 
                          discrete_dim)
    {
      return mcr.EvaluateFunction(numArgsOut, "trainMacro", FinalTree_in1, Finalmecro_in1, FinalMECRO_in1, k, Trains, T, rata, inc_node, discrete_dim);
    }


    /// <summary>
    /// Provides an interface for the trainMacro function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// UNTITLED4 Summary of this function goes here
    /// Detailed explanation goes here
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void trainMacro(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("trainMacro", numArgsOut, ref argsOut, argsIn);
    }



    /// <summary>
    /// This method will cause a MATLAB figure window to behave as a modal dialog box.
    /// The method will not return until all the figure windows associated with this
    /// component have been closed.
    /// </summary>
    /// <remarks>
    /// An application should only call this method when required to keep the
    /// MATLAB figure window from disappearing.  Other techniques, such as calling
    /// Console.ReadLine() from the application should be considered where
    /// possible.</remarks>
    ///
    public void WaitForFiguresToDie()
    {
      mcr.WaitForFiguresToDie();
    }



    #endregion Methods

    #region Class Members

    private static MWMCR mcr= null;

    private static Exception ex_= null;

    private bool disposed= false;

    #endregion Class Members
  }
}
