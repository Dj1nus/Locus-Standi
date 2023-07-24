public class SoundPool
{
    private Sound[] _freeSounds;

    public SoundPool(int value)
    {
        _freeSounds = new Sound[value];
    }

    public Sound GetFreeSound()
    {
        foreach (Sound s in _freeSounds)
        {
            if (s._isFree) return s;
        }

        return _freeSounds[0];
    }
}
