using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InteractableUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Flags]
    public enum MouseClickEvent
    {
        None = 0,
        LeftMouseButton = 1,
        RightMouseButton = 2,
        MiddleMouseButton = 4
    }

    [Flags]
    public enum InteractableUIOptions
    {
        None = 0,
        RemoveImageOnHighlight = 1,
        RemoveImageOnToggle = 2,
        RemoveHighlightOnToggle = 4
    }

    // Events that other classes can subscribe to
    public delegate void MouseEvent(GameObject interactedUI, PointerEventData eventData);
    public MouseEvent OnEnter;
    public MouseEvent OnExit;
    public MouseEvent OnClicked;

    [Header("Options")]
    [Tooltip("Options defining whether toggle image or highlight image replaces original image.")]
    public InteractableUIOptions options = InteractableUIOptions.None;
    [Tooltip("If this is a button, specify the mouse buttons to use.")]
    public MouseClickEvent mouseButton = MouseClickEvent.None;
    public float scaleOnToggled = 1;
    public bool initialInteractable = true;

    [Header("References")]
    [Tooltip("The main/parent GameObject of a UI element.")]
    public GameObject mainGameObject;

    [Header("Optional")]
    public Image image;
    public GameObject highlightImage;
    public Image toggledImage;
    public Image uninteractableImage;

    private Color initialImageColor;
    private bool inside = false;
    private List<Color> colors = new List<Color>();
    private Image[] images;

    // Use this for player controllers.
    public static bool OnUI
    {
        get
        {
            return (onUICount > 0);
        }
    }
    private static int onUICount = 0;

    private bool interactable = true;
    private bool toggled = false;

    public Sprite Sprite
    {
        set
        {
            image.sprite = value;
        }
        get
        {
            return image.sprite;
        }
    }

    public bool Interactable
    {
        get => interactable;
        set
        {
            if (value == interactable)
                return;

            if (value)
            {
                if (uninteractableImage != null)
                    uninteractableImage.gameObject.SetActive(false);

                interactable = true;
            }
            else
            {
                SetHighlighted(false);

                if (uninteractableImage != null)
                    uninteractableImage.gameObject.SetActive(true);

                OnDisable();

                interactable = false;
            }
        }
    }

    public bool Toggled
    {
        get => toggled;

        set
        {
            if (toggledImage == null)
                throw new Exception("There is no toggled image to toggle.");

            toggledImage.gameObject.SetActive(value);

            if (value)
            {
                if (options.HasFlag(InteractableUIOptions.RemoveHighlightOnToggle) && Highlighted)
                {
                    highlightImage.gameObject.SetActive(false);
                }
            }
            else
            {
                if (inside && !Highlighted)
                {
                    highlightImage.gameObject.SetActive(true);
                }
            }
        }
    }

    private bool Highlighted => (highlightImage != null && highlightImage.gameObject.activeSelf);

    private void Awake()
    {
        if (image != null)
            initialImageColor = image.color;

        if (mainGameObject == null)
            throw new Exception("Cannot have an interactable UI with no main GameObject.");

        images = mainGameObject.GetComponentsInChildren<Image>();

        foreach (Image item in images)
        {
            colors.Add(item.color);
        }

        interactable = !initialInteractable;
        Interactable = initialInteractable;

        if (toggledImage != null)
            Toggled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //AudioManager.instance.Play("click");
        if (Interactable && inside)
        {
            if (OnClicked != null &&
                (eventData.button == PointerEventData.InputButton.Left && mouseButton.HasFlag(MouseClickEvent.LeftMouseButton) ||
                 eventData.button == PointerEventData.InputButton.Right && mouseButton.HasFlag(MouseClickEvent.RightMouseButton) ||
                 eventData.button == PointerEventData.InputButton.Middle && mouseButton.HasFlag(MouseClickEvent.MiddleMouseButton)))
            {
                OnClicked(mainGameObject, eventData);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Interactable)
            return;

        if (!inside)
            onUICount++;

        inside = true;
        if (Interactable)
        {
            SetHighlighted(inside);

            if (OnEnter != null)
                OnEnter(mainGameObject, eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Interactable)
            return;

        if (inside)
            onUICount--;

        inside = false;
        if (Interactable)
        {
            SetHighlighted(inside);

            if (OnExit != null)
                OnExit(mainGameObject, eventData);
        }
    }

    private void SetHighlighted(bool highlight)
    {
        if (!Interactable)
            return;

        // Sanitise input
        if (highlightImage == null)
            return;

        highlightImage.gameObject.SetActive(highlight);
        //if (highlight)
        //    AudioManager.instance.Play("hover");


        if (options.HasFlag(InteractableUIOptions.RemoveImageOnHighlight))
            image.gameObject.SetActive(!highlight);
        if (options.HasFlag(InteractableUIOptions.RemoveHighlightOnToggle) && Toggled)
            highlightImage.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (inside)
        {
            onUICount--;
            inside = false;
        }


        toggled = false;

        SetHighlighted(false);
    }

    private void OnEnable()
    {
        if (!inside)
        {
            SetHighlighted(false);
        }
    }
}