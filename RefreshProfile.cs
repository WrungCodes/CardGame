using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshProfile : MonoBehaviour
{
	public GameObject StaticImage;
	public GameObject AnimaterImage;
	// Start is called before the first frame update
	void Start()
    {
		AnimaterImage.SetActive(false);
		StaticImage.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void GetProfileOfUser()
	{
		StaticImage.SetActive(false);
		AnimaterImage.SetActive(true);
		GetProfile.GetUserProfile(
			(response) => {

				StaticImage.SetActive(true);
				AnimaterImage.SetActive(false);

				ProfileResponse profileResponse = (ProfileResponse)response;

				State.UserProfile = profileResponse.profile;
			},
			(statusCode, error) => {
				StaticImage.SetActive(true);
				AnimaterImage.SetActive(false);
			}
		);


	}
}
