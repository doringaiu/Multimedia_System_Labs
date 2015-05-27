<h2 align = "center">  Laboratory work 2</h2>
<h4> <b>Titile:</b> Sound Manipulation </h4>
<h4> <b>Tasks:</b></h4> 
- Develop desktop application that loads a sound from a file and is capable of playing it, with a
[modification] as well as without it. The modification's numeric parameters are adjustable via UI.
- Playback speed shifted up or down. (params: value)

<h5 align = "center"> IDE </h5>
<p align = "center"> Microsft Visual Studio 2015 , Windows Forms Application (C#), DirectX DirectSound</p>

<h5 align = "center"> Screenshots </h5> <br>
<a href="http://www.imagebam.com/image/559de3412109611" target="_blank"><img align = "center" src="http://thumbnails107.imagebam.com/41211/559de3412109611.jpg" alt="imagebam.com"></a> 
</br>
<h5 align = "center"> Application functionality </h5>
- Open a WAV audio file from the computer and load it in to the program
- Display the file name and format
- Play and pause the audio
- Display the timeline (playback progress)
- Adjust playback speed from 1.9X to 0.1X

<h5 align = "center">How it works</h5>
<p>The playback speed depends on the sampling frequency which is usually 44Khz for the media sounds, so in order to make a sound play faster we have to increase it. Direct Sound allows us to manipulate this value. In our program the vertical slider takes values from 19 to 1 and for playback speed manipulation we divide this value by 10 and multiply with the default sampling frequency. </p>

<h5 align = "center"> Conclusion </h5>
<p> By performing this laboratory work we learned how to work with sound files and manipulate some of it's properties like sampling frequency and also how to work with various audio devices using directx.</p> 
