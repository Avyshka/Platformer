using UnityEngine;

namespace Platformer.Utils
{
    public class ContactPooler
    {
        private ContactPoint2D[] _contacts = new ContactPoint2D[10];
        private const float _collThreshold = 0.6f;
        private int _contactCount;
        private Collider2D _collider;
        
        public bool IsGrounded { get; private set; }
        public bool HasLeftContact { get; private set; }
        public bool HasRightContact { get; private set; }

        public ContactPooler(Collider2D collider)
        {
            _collider = collider;
        }
        
        public void Update()
        {
            IsGrounded = false;
            HasLeftContact = false;
            HasRightContact = false;

            _contactCount = _collider.GetContacts(_contacts);
            for (int i = 0; i < _contactCount; i++)
            {
                if (_contacts[i].normal.y > _collThreshold) IsGrounded = true;
                if (_contacts[i].normal.x > _collThreshold) HasLeftContact = true;
                if (_contacts[i].normal.x < -_collThreshold) HasRightContact = true;
            }
        }
    }
}