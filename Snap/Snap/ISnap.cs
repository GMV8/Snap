namespace Snap
{
    public interface ISnap
    {
        int Packs { get; set; }
        SnapConditions SnapMethod { get; set; }

        void Initialise();
        void Play();
        void PrintResult();
    }
}