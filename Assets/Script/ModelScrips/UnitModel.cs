using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.Model.Unit
{
    public class UnitModel
    {
        private int id;
        private int maxHP;
        private int currentHP;
        private int movementRange;
        private int movementPoints;
        private int attackRange;
        private int defense;
        private int attackPower;
        private int maxAttackPoints;
        private Team myTeam;

        private Action OnDamageReceived;
        private Action OnHealReceived;
        private Action OnDeath;

        public UnitModel(ActorModelEntityStats stats, Team team)
        {
            id = stats.ID;
            maxHP = stats.HP;
            currentHP = maxHP;
            movementRange = stats.MovementRange;
            attackRange = stats.AttackRange;
            attackPower = stats.AttackPower;
            maxAttackPoints = stats.AttackPoins;
            defense = stats.Defense;
            myTeam = team;
        }

        public void SubscribeToDamage(Action onDmgAction)
        {
            OnDamageReceived += onDmgAction;
        }

        public void SubscribeToHeal(Action onHealRecieved)
        {
            OnHealReceived += onHealRecieved;
        }

        public void SubscribeToDeath(Action onDeath)
        {
            OnDeath += onDeath;
        }

        public void Death()
        {
            OnDeath?.Invoke();

            CleanUp();
        }

        private void CleanUp()
        {
            OnDamageReceived = null;
            OnHealReceived = null;
            OnDeath = null;
        }

        public void ApplyDamage(int dmg)
        {
            int lifeLoss = Mathf.Clamp(currentHP - dmg, 0, maxHP);

            currentHP -= lifeLoss;

            OnDamageReceived?.Invoke();
        }

        public void Heal(int heal)
        {
            int lifeGain = Mathf.Clamp(currentHP + heal, 0, maxHP);

            currentHP += lifeGain;

            OnHealReceived?.Invoke();
        }
    }
}

