using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GoC {
    public enum TroopClass {
        Melee = 0,
        Ranger, Shield, Rider, Magic
    }
    public enum Kingdom{
        Default = 0, G,H,J,K
    }
        
    
    public enum LayerObject{
        Decorations = 2,Base,Houses,Resources
    }
    public enum ResourceType {
        Wood = 1,Stone= 2
    }

    public enum FloorType {
        Default = 0,Dirt = 1,
        Grass, Sand, Stone
    }
    public struct Floor{
        public FloorType type;
        public TileBase tile;
        public Vector3Int base_pos;
    }


    public struct Loot{
        public string owner;
        public bool isLootable;
        public int Coin;
        public int ScorePoint;
    }
    public struct Resource{
        public Floor floor;
        public ResourceType type;
        public int Quality;
        public int TurnCreated;
        public Loot loot;
    }
    public struct LifeStruct {
        public int Health;
        public int Armor;
        public int Attack;
        public int Cost;
    }
    public class Player{
        public PlayerData data;
        public int Coins;
        public int Score;
        public int TurnsNumber;
        public Player(PlayerData data){
            this.data = data; 
            Coins = 100;
            Score = 0;
            TurnsNumber = 0;
        }

    }
    public class Troop {
        public static int count;
        public Tilemap MemberOf;
        public Vector3Int GridPosition;
        public Vector3 WorldPosition;
        public TroopClass Class;
        public LifeStruct Life;
        public Troop (TroopClass c) {
            Class = c;
            Life = new LifeStruct () {
                Health = 3,
                Armor = 0,
                Attack = 1,
                Cost = 2
            };
            count++;
        }

        ~Troop () {
            --count;
        }
    }
}