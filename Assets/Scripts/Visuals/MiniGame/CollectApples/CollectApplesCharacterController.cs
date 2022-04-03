using UnityEngine;

namespace LudumDare50.Client.Visuals.MiniGame.CollectApples
{
    public class CollectApplesCharacterController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private bool _prevStatusVertical;
        private bool _prevStatusHorizontal;
        private bool _moveHorizontal;

        private void Update()
        {
            var statusHorizontal = Input.GetButton("Horizontal");
            var statusVertical = Input.GetButton("Vertical");

            if (statusHorizontal && !_prevStatusHorizontal)
                _moveHorizontal = true;
            if (statusVertical && !_prevStatusVertical || !statusHorizontal)
                _moveHorizontal = false;

            if (statusVertical && !_moveHorizontal)
            {
                _animator.SetBool("IsMoving", true);
                var verticalMovement = Input.GetAxis("Vertical");
                _animator.SetFloat("Vertical", verticalMovement);
                _animator.SetFloat("Horizontal", 0);
                transform.localPosition += new Vector3(0, verticalMovement * Time.deltaTime * 3, 0);
                if (transform.localPosition.y > 3.6f)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, 3.6f, transform.localPosition.z);
                }
                else if (transform.localPosition.y < -1.55f)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, -1.55f, transform.localPosition.z);
                }
            }
            else if (statusHorizontal)
            {
                _animator.SetBool("IsMoving", true);
                var horizontalMovement = Input.GetAxis("Horizontal");
                _animator.SetFloat("Vertical", 0);
                _animator.SetFloat("Horizontal", horizontalMovement);
                transform.localPosition += new Vector3(horizontalMovement * Time.deltaTime * 3, 0, 0);
                if(transform.localPosition.x > 4.5f)
                {
                    transform.localPosition = new Vector3(4.5f, transform.localPosition.y, transform.localPosition.z);
                }
                else if(transform.localPosition.x < -4.5f)
                {
                    transform.localPosition = new Vector3(-4.5f, transform.localPosition.y, transform.localPosition.z);
                }
            }
            else
            {
                _animator.SetBool("IsMoving", false);
                _animator.SetFloat("Vertical", 0);
                _animator.SetFloat("Horizontal", 0);
            }
        }
    }
}