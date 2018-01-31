﻿public class RespiratoryData    
{
    private float _ipf, _epf, _ift, _eft, _rf;

    public float InspiratoryPeakFlow
    {
        get { return _ipf * GameMaster.Mercy; }
        set { _ipf = value; }
    }

    public float ExpiratoryPeakFlow
    {
        get { return _epf * GameMaster.Mercy; }
        set { _epf = value; }
    }

    /// <summary>
    /// Inspiratory Flow Time (in milliseconds)
    /// </summary>
    public float InspiratoryFlowTime
    {
        get { return _ift * GameMaster.Mercy; }
        set { _ift = value; }
    }

    /// <summary>
    /// Expiratory Flow Time (in milliseconds)
    /// </summary>
    public float ExpiratoryFlowTime
    {
        get { return _eft * GameMaster.Mercy; }
        set { _eft = value; }
    }

    /// <summary>
    /// Respiration Frequency (mean time of one cycle, in milliseconds)
    /// </summary>
    public float RespirationFrequency
    {
        get { return _rf / GameMaster.Mercy; }
        set { _rf = value; }
    }

    public void ResetRespiratoryInfo()
    {
        _ipf = 0f;
        _epf = 0f;
        _ift = 0f;
        _eft = 0f;
        _rf = 0f;
    }

    /// <summary>
    /// Returns raw sensor/respiratory values calibrated from the player.
    /// </summary>
    /// <returns></returns>
    public RawRespiratoryInfo GetRawInfo()
    {
        return new RawRespiratoryInfo
        {
            InspiratoryFlowTime = _ift,
            ExpiratoryFlowTime = _eft,
            ExpiratoryPeakFlow = _epf,
            InspiratoryPeakFlow = _ipf,
            RespirationFrequency = _rf
        };
    }
}

public struct RawRespiratoryInfo
{
    public float InspiratoryPeakFlow, ExpiratoryPeakFlow, InspiratoryFlowTime, ExpiratoryFlowTime, RespirationFrequency;
}