using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GoC {
    public interface Data {
        bool IsDataInstantiated ();
    }

    public static class Function {
        public static float Round (float value) {
            if (value % 1 >= 0.5)
                return Mathf.Ceil (value);
            else
                return Mathf.Floor (value);
        }
    }

    [System.Serializable]
    public class SettingsData : Data {

        bool isDataInstantiated;
        public float MasterVolume;
        public float MusicVolume;
        public float SoundFXVolume;
        public void SetVolumesData (float a, float b, float c) {
            isDataInstantiated = true;
            MasterVolume = a;
            MusicVolume = b;
            SoundFXVolume = c;
        }
        public bool IsDataInstantiated () {
            return isDataInstantiated;
        }

    }
    public class PlayerData : Data {
        bool isDataInstantiated;
        public string Name;
        public string Password;
        public int playDate;
        public string Kingdom;
        public void Store (string name, string pass, int date) {
            isDataInstantiated = true;
            Name = name;
            Password = pass;
            playDate = date;
        }
        public bool IsDataInstantiated () {
            return isDataInstantiated;
        }
    }
    public class Players {
        List<PlayerData> Data;
        public int Count;
        public Players () {
            Count = 0;
            Data = new List<PlayerData> ();
        }
        public void Add (PlayerData data) {
            ++Count;
            Data.Add (data);
        }

        public void RemoveLast () {
            Data.RemoveAt (Count - 1);
            --Count;
        }

        public void RemoveAll () {
            Data.RemoveRange (0, Count);
            Count = 0;
        }
        public bool IsPlayerExist (string name) {
            foreach (var player in Data) {
                if (player.Name == name) {
                    return true;
                }
            }
            return false;
        }

        public PlayerData this [int index] {
            get { return Data[index]; }
            set { Data[index] = value; }
        }
    }
    public interface ITransition {
        IEnumerator TransitionUIElement (MaskableGraphic element, float start, float end, float delay = 0, float lerpTime = 0.5f);
    }
    public struct TileLocation {
        int x, y;
    }

    public struct Limit {

        public float x, y;

        public Limit (float _xy) {
            this.x = _xy;
            this.y = _xy;
        }
        public Limit (float _x, float _y) {
            this.x = _x;
            this.y = _y;
        }

        public bool CompareTo (float other) {
            var isX = (other < this.x) || (other > this.y);
            if (isX) {
                return false;
            }
            return true;
        }
    }

    public class GObject {
        protected TileLocation location;
        protected int Type;
    }

    public class LObject : GObject {
        protected int LifePoints;
        protected int Attack;
    }

    public interface ITroopunit {
        int Attack ();
        bool Die ();
        int GatherRes ();
    }

}