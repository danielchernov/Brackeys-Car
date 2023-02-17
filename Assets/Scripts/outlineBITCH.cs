//MIT License
//Copyright (c) 2023 DA LAB (https://www.youtube.com/@DA-LAB)
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class outlineBITCH : voiceManager
{
    public voiceManager voiceManager;
    public GameObject[] luggage;

    void Update()
    {
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        ray = carCamera.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.collider.transform;
            if (highlight.CompareTag("Highlightable"))
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                }

                if (
                    highlight.gameObject.name == "Luggage1"
                    || highlight.gameObject.name == "Luggage2"
                )
                {
                    for (int i = 0; i < luggage.Length; i++)
                    {
                        if (luggage[i].GetComponent<Outline>() != null)
                        {
                            luggage[i].GetComponent<Outline>().enabled = true;
                        }
                        else
                        {
                            Outline outline = luggage[i].AddComponent<Outline>();
                            outline.enabled = true;
                        }
                    }
                }

                if (
                    highlight.gameObject.name == "SteeringWheel"
                        && voiceManager.wheelClipNumber >= 3
                    || highlight.gameObject.name == "SteeringWheel" && voiceSource.isPlaying
                    || highlight.gameObject.name == "SteeringWheel" && voiceManager.itemsAreBlocked
                    || highlight.gameObject.name == "Backpack"
                        && voiceManager.backpackClipNumber >= 3
                    || highlight.gameObject.name == "Backpack" && voiceSource.isPlaying
                    || highlight.gameObject.name == "Backpack" && voiceManager.itemsAreBlocked
                    || highlight.gameObject.name == "Books" && voiceManager.booksClipNumber >= 3
                    || highlight.gameObject.name == "Books" && voiceSource.isPlaying
                    || highlight.gameObject.name == "Books" && voiceManager.itemsAreBlocked
                    || highlight.gameObject.name == "RadioOutliner"
                        && voiceSource.isPlaying
                        && voiceManager.radioClipNumber < 3
                    || highlight.gameObject.name == "RadioOutliner"
                        && voiceManager.itemsAreBlocked
                        && voiceManager.radioClipNumber < 3
                    || highlight.gameObject.name == "RadioOutliner"
                        && voiceManager.itemsAreBlocked
                        && voiceManager.mainIsBlocked
                )
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = false;
                }

                if (
                    highlight.gameObject.name == "Luggage1" && voiceManager.luggageClipNumber >= 3
                    || highlight.gameObject.name == "Luggage1" && voiceSource.isPlaying
                    || highlight.gameObject.name == "Luggage1" && voiceManager.itemsAreBlocked
                    || highlight.gameObject.name == "Luggage2"
                        && voiceManager.luggageClipNumber >= 3
                    || highlight.gameObject.name == "Luggage2" && voiceSource.isPlaying
                    || highlight.gameObject.name == "Luggage2" && voiceManager.itemsAreBlocked
                )
                {
                    for (int i = 0; i < luggage.Length; i++)
                    {
                        if (luggage[i].GetComponent<Outline>() != null)
                        {
                            luggage[i].GetComponent<Outline>().enabled = false;
                        }
                        else
                        {
                            Outline outline = luggage[i].AddComponent<Outline>();
                            outline.enabled = false;
                        }
                    }
                }
            }
            else
            {
                highlight = null;
            }

            Outline luggage1 = luggage[0].GetComponent<Outline>();
            Outline luggage2 = luggage[1].GetComponent<Outline>();

            if (luggage1 != null && !luggage1.enabled || luggage2 != null && !luggage2.enabled)
            {
                for (int i = 0; i < luggage.Length; i++)
                {
                    luggage[i].GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}
