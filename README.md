# Neural-Units
A neural network frame built in C#, Easy to be understood, learn and use.

it's NOT based on any other project, like Tensorflow.

```csharp
//import NeuralUnits.dll into project
using NeuralUnits;

//create builder
Builder b = new Builder();

//create a unit layer named "I1", width=78, height=78, depth=1
b.create_unit_layer("I1", 78, 78, 1);

//create many layers
b.create_unit_layer("C2", 76, 76, 60);
b.create_unit_layer("P3", 38, 38, 60);
b.create_unit_layer("C4", 36, 36, 90);
b.create_unit_layer("P5", 18, 18, 90);
b.create_unit_layer("C6", 16, 16, 120);
b.create_unit_layer("P7", 8, 8, 120);
b.create_unit_layer("C8", 6, 6, 240);
b.create_unit_layer("P9", 3, 3, 240);
b.create_unit_layer("C10", 1, 1, 480);
b.create_unit_layer("F11", 1, 1, 960);
b.create_unit_layer("F12", 1, 1, 1);

//connect "I1" to "C2" with convolution, size of kernel is 3x3
b["I1"].create_convolution(b["C2"], 3, 3);

//connecting other layers
b["P3"].create_convolution(b["C4"], 3, 3);
b["P5"].create_convolution(b["C6"], 3, 3);
b["P7"].create_convolution(b["C8"], 3, 3);
b["P9"].create_convolution(b["C10"], 3, 3);

//connect "C10" to "F11" with fully connection, size of kernel is 3x3
b["C10"].create_fully_connection(b["F11"]);

//connecting other layers
b["F11"].create_fully_connection(b["F12"]);

//begin training, Forward propagation!
b.clear_all_gradients_of_last_training();
b["I1"].input_samples(p.get_pixels());
b["I1"].rise();
b["I1"].forward_convolute(b["C2"], 1);
b["C2"].SELU();
b["C2"].forward_max_pool(b["P3"], 2, 2, 2);
b["P3"].rise();
b["P3"].forward_convolute(b["C4"], 1);
b["C4"].SELU();
b["C4"].forward_max_pool(b["P5"], 2, 2, 2);
b["P5"].rise();
b["P5"].forward_convolute(b["C6"], 1);
b["C6"].SELU();
b["C6"].forward_max_pool(b["P7"], 2, 2, 2);
b["P7"].rise();
b["P7"].forward_convolute(b["C8"], 1);
b["C8"].SELU();
b["C8"].forward_max_pool(b["P9"], 2, 2, 2);
b["P9"].rise();
b["P9"].forward_convolute(b["C10"], 1);
b["C10"].SELU();
b["C10"].forward_fully_connect(b["F11"]);
b["F11"].SELU();
b["F11"].forward_fully_connect(b["F12"]);
b["F12"].atan();

//find the loss
b["F12"].find_loss_by_MSE(labels);

//Backward propagation!
b["F12"].diff_atan();
b["F12"].backward_fully_connect(b["F11"]);
b["F11"].diff_SELU();
b["F11"].backward_fully_connect(b["C10"]);
b["C10"].diff_SELU();
b["C10"].backward_convolute(b["P9"], 1);
b["P9"].diff_rise();
b["P9"].backward_max_pool(b["C8"], 2, 2, 2);
b["C8"].diff_SELU();
b["C8"].backward_convolute(b["P7"], 1);
b["P7"].diff_rise();
b["P7"].backward_max_pool(b["C6"], 2, 2, 2);
b["C6"].diff_SELU();
b["C6"].backward_convolute(b["P5"], 1);
b["P5"].diff_rise();
b["P5"].backward_max_pool(b["C4"], 2, 2, 2);
b["C4"].diff_SELU();
b["C4"].backward_convolute(b["P3"], 1);
b["P3"].diff_rise();
b["P3"].backward_max_pool(b["C2"], 2, 2, 2);
b["C2"].diff_SELU();
b["C2"].backward_convolute(b["I1"], 1);

//print loss
double loss = b["F12"].report_loss(labels);
```

## it have activtion functions below
ELU();

ReLU();

SELU();

sigmoid();

tanh();

Neural-Units can't run on GPU.
