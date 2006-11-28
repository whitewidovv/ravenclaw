using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

using RavenClaw.MudEngine.Interfaces;
namespace RavenClaw.MudEngine.Data
{
    public class Account
    {
        private Account()
        {

        }

        private Account(string username, string password, IRace race)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            if (race == null)
            {
                throw new ArgumentNullException("race");
            }

            this.Username = username;
            this.Race = race;

            SetPassword(password);

        }

        #region Properties
        private bool _dirty = false;

        private int _accountId;

        public int AccountId
        {
            get
            {
                return _accountId;
            }
        }

        private string _username;

        public string Username
        {
            get
            {
               return _username;
            }

            set
            {
                _dirty |= true;
                _username = value;
            }

        }

        private string _passwordHash;

        public string PasswordHash
        {
            get
            {
                return _passwordHash;
            }
        }

        private IRace _race;

        public IRace Race
        {
            get
            {
                return _race;
            }
            set
            {
                _dirty |= true;
                _race = value;
            }
        }

        private string _bio;

        public string Bio
        {
            get
            {
                return _bio;
            }
            set
            {
                _dirty |= true;
                _bio = value;
            }
        }

        private IPlane _plane;
        public IPlane Plane
        {
            get
            {
                return _plane;
            }
            set
            {
                _dirty |= true;
                _plane = value;
            }
        }

        private Int64 _hitPoints;
        public Int64 HitPoints
        {
            get
            {
                return _hitPoints;
            }
            set
            {
                _dirty |= true;
                _hitPoints = value;
            }
        }


        private Int64 _mana;
        public Int64 Mana
        {
            get
            {
                return _mana;
            }
            set
            {
                _dirty |= true;
                _mana = value;
            }
        }
        #endregion

        /// <summary>
        /// Sets the password against the account to a new value.
        /// </summary>
        /// <param name="password">The password to set the account's password to.</param>
        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            SHA1Managed sha1 = new SHA1Managed();

            // We hash the accountId so that it acts as a salt.
            byte[] passwordBlobToHash = System.Text.ASCIIEncoding.ASCII.GetBytes(this._accountId.ToString() + password);

            byte[] hashResult = sha1.ComputeHash(passwordBlobToHash);

            this._passwordHash = BitConverter.ToString(hashResult);

            this.Persist();
        }

        /// <summary>
        /// Writes the Account instance back to the database.
        /// </summary>
        public void Persist()
        {
            if (_dirty)
            {
                DbCommand command = DatabaseEngine.Instance.CreateCommand();

                command.CommandText = "update Account set Bio=@Bio, HitPoints=@HitPoints, Mana=@Mana, PasswordHash=@PasswordHash, PlaneId=@PlaneId, RaceId=@RaceId where AccountId=@AccountId";

                DatabaseEngine.AddLongParameter(command, "@AccountId", this._accountId);
                       
                DatabaseEngine.AddLongParameter(command, "@HitPoints", this._hitPoints);
                DatabaseEngine.AddLongParameter(command, "@PlaneId", this._plane.Elevation);
                DatabaseEngine.AddLongParameter(command, "@RaceId", this._race.RaceId);
                DatabaseEngine.AddLongParameter(command, "@Mana", this._mana);

                DatabaseEngine.AddStringParameter(command, "@Bio", this._bio);
                DatabaseEngine.AddStringParameter(command, "@PasswordHash", this._passwordHash);
                
                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Removes the instance from the database.
        /// </summary>
        public void Remove()
        {
            DbCommand command = DatabaseEngine.Instance.CreateCommand();

            command.CommandText = "delete from Account where AccountId=@AccountId";

            DatabaseEngine.AddLongParameter(command, "@AccountId", this._accountId);

            command.ExecuteNonQuery();

        }
        
        public static Account Create(string username, string password, IRace race)
        {
            Account account = new Account(username, password, race);

            account._accountId = 1;
            account._hitPoints = 0;
            return account;
        }

    }
}
