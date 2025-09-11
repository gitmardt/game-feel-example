using UnityEngine;

public static class SlowMotion
{
    private class Runner : MonoBehaviour { }
    private static Runner _runner;
    private static Coroutine _current;

    private static Runner GetRunner()
    {
        if (_runner == null)
        {
            var go = new GameObject("~TimeEffects");
            Object.DontDestroyOnLoad(go);
            _runner = go.AddComponent<Runner>();
        }
        return _runner;
    }

    public static void SlowMoPulse(float scale, float duration)
    {
        var r = GetRunner();
        if (_current != null) r.StopCoroutine(_current);
        _current = r.StartCoroutine(SlowMoCo(Mathf.Clamp(scale, 0.01f, 1f), Mathf.Max(0f, duration)));
    }

    private static System.Collections.IEnumerator SlowMoCo(float scale, float duration)
    {
        float originalScale = Time.timeScale;
        float originalFixed = Time.fixedDeltaTime;

        Time.timeScale = scale;
        Time.fixedDeltaTime = originalFixed * (Time.timeScale / Mathf.Max(originalScale, 0.0001f));

        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        // Restore
        Time.timeScale = originalScale;
        Time.fixedDeltaTime = originalFixed;
        _current = null;
    }
}
