package com.example.apkasportowca;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Button btnZap = (Button) findViewById(R.id.buttonKalk);
        Button btnMax = (Button) findViewById(R.id.buttonMax);
        Button btnBmi = (Button) findViewById(R.id.buttonBmi);
        Button btnWaga = (Button) findViewById(R.id.buttonWaga);

        btnZap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent Zap = new Intent (getApplicationContext(), Zapo.class);
                startActivity(Zap);
            }
        });

        btnMax.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent Max = new Intent (getApplicationContext(), Max.class);
                startActivity(Max);
            }
        });

        btnBmi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent Bmi = new Intent (getApplicationContext(), Bmi.class);
                startActivity(Bmi);
            }
        });

        btnWaga.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent Wag = new Intent (getApplicationContext(), Waga.class);
                startActivity(Wag);
            }
        });
    }
}
