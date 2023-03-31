using System.Collections.Concurrent;

using Terraria;

namespace SaiyanTails.Utils;

internal class IncrementingConcurrentDictionary<K> where K : notnull {
    private readonly ConcurrentDictionary<K, long> m_BackingField;

    internal IncrementingConcurrentDictionary() {
        m_BackingField = new();
    }

    internal long this[K key] {
        get {
            if (!m_BackingField.ContainsKey(key))
                m_BackingField[key] = 0;


            if (!Main.GlobalTimerPaused && !Main.gamePaused)
                return m_BackingField[key]++;

            return m_BackingField[key];
        }
        set {
            m_BackingField[key] = value;
        }
    }
}