using LibraryApp.Shared.DTOs;

namespace LibraryApp.Reader.State;

public class ReaderSessionState
{
    public ReaderDTO? CurrentReader { get; private set; }
    public bool IsLoggedIn => CurrentReader is not null;

    public event Action? OnChange;

    public void SetReader(ReaderDTO reader)
    {
        CurrentReader = reader;
        OnChange?.Invoke();
    }

    public void Clear()
    {
        CurrentReader = null;
        OnChange?.Invoke();
    }
}
