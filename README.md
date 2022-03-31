# GlowMarkt Meter Data Microservice

This project is designed to integrate near-realtime smart meter data captured using a [GlowMarkt In-Home Display (IHD/CAD)](https://shop.glowmarkt.com/products/display-and-cad-combined-for-smart-meter-customers) over MQTT. I assume it will work with the [GlowMarkt Glow Stick](https://shop.glowmarkt.com/products/glow-stick), too.

The code herein receives the rather cumbersome zigbee Smart Energy Profile data, encoded in JSON, from GlowMarkt's MQTT broker and emits a reduced, but far easier to consume, JSON payload to a different MQTT broker.

## Health Warning
I only received my GlowMarkt IHD today and this is the result of just a few hours work. It's functioning nicely for me, at the moment, but the code may be brittle; it doesn't take much care over runtime errors. So it might not work as-is, for you. Or it might work now but break if the data from GlowMarkt changes. Of course, there are not guarantees. Fixes or improvements would be most welcome!

## Technology Stack

The project was developed using C# and .NET 6.0 to run on Raspberry Pi. Specifically, I package my applications as microservices and run them on a K3S Kubernetes cluster.

However, the assets here will allow you to run the code on Windows/Mac/Linux either natively, under Docker or under Kubernetes.

The included Dockerfile builds an image targeting 32-bit ARM for my Raspberry Pi cluster. My cluster is actually a hybrid with 32-bit and 64-bit nodes. Changing the Dockerfile to target 64-bit ARM (or any other relevant architecture supported by .NET 6.0) should still work.

## Project Structure

Most of the classes in this project are purely to support deserializing the JSON from the GlowMarkt MQTT message. If you look at the relevant code (start with GlowMqttMessage and work down), you'll see that most properties are annotated with JsonPropertyNames that have numeric names. This is how the incoming JSON is structured and is needed for the deserializer. Within our code, it's preferable to refer to properties using much more meaningful names. Most of the data in the incoming message is hexadecimal. The code takes care of the necessary type conversions to provide a more reasonable representation.

The OutgoingMeteringMessage class defines the MQTT message that is derived from the GlowMarkt MQTT message and is posted to a specified broker and topic.

## Configuration

The basic configuration is defined in appsettings.json. You can update this file for your MQTT settings, but I recommend that you use a different mechanism.

The app can be configured with environment variables, command-line arguments or with a separate secrets.json file. I use .NET 6.0 configuration, adopting the Options pattern.

When deployed to Kubernetes, I override the default configuration with environment variables. The sensitive settings are sourced from a Kubernetes secret. See Deployment.yaml for an example of Kubernetes configuration. I'm keeping my secret, secret, though.





