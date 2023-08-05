using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


namespace Player
{
    public class RotationKnife : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private MyInputsManager myInputsManager;

        [Header("Values Knife")]    
        public Vector3 directionKnife;
        public float jumpForce;
        public float kickForceMultiplier;
        private bool canImpulse = false;
    
        private Rigidbody rig;
        private bool isRotating;
        private float initialRotationZ;

        void Start()
        {
            rig = GetComponent<Rigidbody>();

            rig.isKinematic = true;     
        }

        void Update()
        {
            if (myInputsManager.myInputs.Knife.Rotation.triggered && !isRotating)
            {
                initialRotationZ = transform.localEulerAngles.z;
                canImpulse = true;
                isRotating = true;
                rig.isKinematic = false;  
                RotateKnife();
            }       
        }

        public void FixedUpdate()
        {
            if(canImpulse)
            {
                KickAndFlip();
            }      
        }
        private void KickAndFlip()
        {
            float verticalVelocity = rig.velocity.y;

            // Se o objeto estiver indo para cima, aplicamos a for�a normalmente
            if (verticalVelocity >= 0)
            {
                rig.AddForce(jumpForce * directionKnife, ForceMode.Impulse);
            }
            else if(verticalVelocity < 0)
            {
                // Se o objeto estiver caindo, aumentamos a for�a para compensar a gravidade
                rig.AddForce(jumpForce * kickForceMultiplier * directionKnife, ForceMode.Impulse);
            }

            canImpulse = false;
        }

        public Quaternion GetTransformRotation()
        {
            return transform.rotation;
        }

        private void RotateKnife()
        {
            float targetRotationZ = initialRotationZ - 360f;
            
            transform.DOLocalRotate(new Vector3(0f, 0f, targetRotationZ), .5f, RotateMode.FastBeyond360)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                transform.localEulerAngles = new Vector3(0f, 0f, targetRotationZ);
                isRotating = false;
            });
        }
    }
}

