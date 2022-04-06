using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransformManager : MonoBehaviour
{
    [SerializeField] private GameObject _cheese;
    [SerializeField] private GameObject _cheeseRenderer;
    [SerializeField] private GameObject _pie;
    [SerializeField] private GameObject _pieRenderer;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _circleRenderer;

    [SerializeField] private List<GameObject> _heart;
    [SerializeField] private List<GameObject> _star;
    [SerializeField] private List<GameObject> _snow;

    [SerializeField] private List<GameObject> _lastParticle;

    [SerializeField] private ParticleSystem _snowParticle;
    [SerializeField] private ParticleSystem _heartParticle;
    [SerializeField] private ParticleSystem _starParticle;

    [SerializeField] private ParticleSystem _heartExplosion;
    [SerializeField] private ParticleSystem _snowExplosion;
    [SerializeField] private ParticleSystem _starExplosion;

    [SerializeField] private GameObject _spawnPos;


    public ParticleSystem particle;
    [SerializeField] private FinishMovement _finishMovement;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;

    private void OnTriggerEnter(Collider other)
    {
        #region Kaybetme
        if (other.tag == "Saw" || other.tag == "Spikes")
        {
            if(_lastParticle.Count != 0)
            {
                StartCoroutine(StopParticle(_lastParticle[0].name));
            }
            StartCoroutine(DisableCamera());
            transform.DOJump(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z - 2),3f,1,1f);
            if(other.tag == "Spikes")
            {
                Destroy(other.gameObject.GetComponent<Collider>());
                other.transform.DOMoveY(-1f, 2f);
            }   
            if (_lastParticle.Count != 0)
            {
                for(int i = 0; i < _lastParticle.Count; i++)
                {

                    _lastParticle[i].SetActive(false);
                }
            }
            _lastParticle.Clear();
        }
        if(other.tag == "Space")
        {
            Destroy(Camera.main.gameObject.GetComponent<CameraScript>());
            Destroy(gameObject.GetComponent<PlayerMovement>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<Collider>().isTrigger = true;
            Vector3 currentPos = transform.position;
            Vector3 diePos = new Vector3(transform.position.x, -120f, transform.position.z);
            Debug.Log("Oyunu bitir");
            transform.DOMoveY(-6f, 1.5f).SetEase(Ease.InOutElastic);
        }
        #endregion
        #region Sekil Degistirme
        if (other.gameObject.tag == "Comb")
        {
            StartCoroutine(DisableCamera());
            PlayerMovement._stopTime = 2f;
            if (gameObject.layer == 8)
            {
                _circle.SetActive(false);
            }
            else if (gameObject.layer == 9)
            {
                _pie.SetActive(false);
            }
            _cheese.SetActive(true);
            particle.Play();
            gameObject.layer = 10;
        }
        if (other.tag == "Cube")
        {
            StartCoroutine(DisableCamera());
            PlayerMovement._stopTime = 2f;
            if (gameObject.layer == 10)
            {
                _cheese.SetActive(false);
            }
            else if (gameObject.layer == 9)
            {
                _pie.SetActive(false);
            }
            _circle.SetActive(true);
            particle.Play();
            gameObject.layer = 8;
        }
        if (other.tag == "Clasp")
        {
            StartCoroutine(DisableCamera());
            PlayerMovement._stopTime = 2f;
            if (gameObject.layer == 8)
            {
                _circle.SetActive(false);
            }
            else if (gameObject.layer == 10)
            {
                _cheese.SetActive(false);
            }
            _pie.SetActive(true);
            particle.Play();
            gameObject.layer = 9;
        }
        #endregion
        #region Dokme islemi
        if (other.tag == "Star")
        {
            if (_lastParticle.Count != 0)
            {
                _lastParticle.Clear();
            }
            for (int i = 0; i < _star.Count; i++)
            {
                _star[i].SetActive(true);
                _lastParticle.Add(_star[i]);
            }
            if (!_starExplosion.gameObject.activeInHierarchy)
            {
                _starExplosion.gameObject.SetActive(true);
            }
            else
            {
                _starExplosion.Play();
            }
        }
        if (other.tag == "Heart")
        {
            if (_lastParticle.Count != 0)
            {
                _lastParticle.Clear();
            }
            for (int i = 0; i < _heart.Count; i++)
            {
                _heart[i].SetActive(true);
                _lastParticle.Add(_heart[i]);
            }
            if (!_heartExplosion.gameObject.activeInHierarchy)
            {
                _heartExplosion.gameObject.SetActive(true);
            }
            else
            {
                _heartExplosion.Play();
            }
        }
        if(other.tag == "Snow")
        {
            if (_lastParticle.Count != 0)
            {
                _lastParticle.Clear();
            }
            for (int i = 0; i < _snow.Count; i++)
            {
                _snow[i].SetActive(true);
                _lastParticle.Add(_snow[i]);
            }
            if (!_snowExplosion.gameObject.activeInHierarchy)
            {
                _snowExplosion.gameObject.SetActive(true);
            }
            else
            {
                _snowExplosion.Play();
            }
        }
        #endregion
        #region Renk Degistirme
        if (other.tag == "RedColor")
        {
            _circleRenderer.GetComponent<Renderer>().material = red;
            _cheeseRenderer.GetComponent<Renderer>().material = red;
            _pieRenderer.GetComponent<Renderer>().material = red;
        }
        if (other.tag == "BlueColor")
        {
            _circleRenderer.GetComponent<Renderer>().material = blue;
            _cheeseRenderer.GetComponent<Renderer>().material = blue;
            _pieRenderer.GetComponent<Renderer>().material = blue;
        }
        #endregion
        #region Bitis
        if(other.tag == "Freeze")
        {
            PlayerMovement._stopTime = 2f;
            StartCoroutine(DisableCamera());
            Destroy(_cheeseRenderer.GetComponent<JellyMesh>());
            Destroy(_circleRenderer.GetComponent<JellyMesh>());
            Destroy(_pieRenderer.GetComponent<JellyMesh>());
        }
        if(other.tag == "Finish")
        {
            StartCoroutine(FinishSpawning());
            
        }
        #endregion
    }
    public IEnumerator DisableCamera()
    {
        Camera.main.gameObject.GetComponent<CameraScript>().enabled = false;
        yield return new WaitForSeconds(1f);
        Camera.main.gameObject.GetComponent<CameraScript>().enabled = true;
    }
    public IEnumerator StopParticle(string lastparticle)
    {
        if(lastparticle == "Heart")
        {
            _heartParticle.gameObject.SetActive(true);
        }
        if(lastparticle == "Snow")
        {
            _snowParticle.gameObject.SetActive(true);
        }
        if(lastparticle == "Star")
        {
            _starParticle.gameObject.SetActive(true);

        }
        yield return new WaitForSeconds(1f);
        _snowParticle.gameObject.SetActive(false);
        _heartParticle.gameObject.SetActive(false);
        _starParticle.gameObject.SetActive(false);
    }
    public IEnumerator FinishSpawning()
    {
        transform.DOMoveX(0, 1f);
        PlayerMovement.isFinish = true;
        _heartExplosion.gameObject.SetActive(false);
        _snowExplosion.gameObject.SetActive(false);
        _starExplosion.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        transform.DOMove(_spawnPos.transform.position, 1f);
        yield return new WaitForSeconds(1f);
        var currentGameObject = Instantiate(gameObject, _spawnPos.transform.position, Quaternion.Euler(-90f, 0, 0));
        Destroy(currentGameObject.gameObject.GetComponent<Rigidbody>());
        Destroy(currentGameObject.gameObject.GetComponent<PlayerMovement>());
        Destroy(currentGameObject.gameObject.GetComponent<TransformManager>());
        Destroy(gameObject);
    }
}
