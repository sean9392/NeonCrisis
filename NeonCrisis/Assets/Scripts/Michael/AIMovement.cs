using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIMovement : MonoBehaviour {

    [System.Serializable]
    public struct InstructionSet
    {
        public AnimationClip anim;
        public float delay;
    }

    private Animation anim;

	

    public List<InstructionSet> Instructions = new List<InstructionSet>();
    

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        anim.Play(Instructions[0].anim.name);

    }

    // Update is called once per frame
    void Update () {


        if(!anim.isPlaying)
        {
            this.gameObject.transform.parent.transform.position = this.transform.position;
            this.transform.localPosition = Vector3.zero;
            if(Instructions.Count >= 1)
            {

                Instructions.RemoveAt(0);
                if(Instructions.Count >= 1)
                anim.Play(Instructions[0].anim.name);
            }
        }
        else
        {
            
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(this.gameObject.transform.parent.position, Vector3.one);

    }

    /*
        void StringIdea()
        {

            if (timer > 2)
            {

                Instruction = Instruction.Substring(1, Instruction.Length - 1);
                timer = 0;
                Debug.Log(Instruction[0]);
            }

            switch (Instruction[0].ToString())
            {

                case "a":
                    this.transform.Translate(Vector3.left * 0.01f);
                    break;
                case "b":
                    this.transform.Translate(Vector3.right * 0.01f);
                    break;
                case "c":
                    this.transform.Translate(Vector3.up * 0.01f);
                    break;
                case "d":
                    this.transform.Translate(Vector3.down * 0.01f);
                    break;
            }
        }
    */
}
