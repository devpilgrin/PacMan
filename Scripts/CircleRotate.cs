﻿using UnityEngine;
            if (RotateBackwards) transform.Rotate(Vector3.up * Time.deltaTime * RotateSpeed);
            else transform.Rotate(Vector3.down * Time.deltaTime * RotateSpeed);