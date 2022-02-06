package com.example.apkasportowca;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Zapo extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_zapo);

        final EditText editTextWag= (EditText) findViewById(R.id.editTextWag);
        final EditText editTextWzr= (EditText) findViewById(R.id.editTextWzr);
        final EditText editTextWiek= (EditText) findViewById(R.id.editTextWiek);
        final Spinner spinnerAkt=(Spinner) findViewById(R.id.spinnerAkt);
        final Spinner spinnerPlec=(Spinner) findViewById(R.id.spinnerPlec);
        Button btnObl=(Button) findViewById(R.id.buttonObl);
        final TextView textZapo=(TextView) findViewById(R.id.textView6);

        String[] Aktywnosc = new String[]{"Siedzący tryb życia.", "Umiarkowana aktywność fizyczna.", "Średnia aktywność fizyczna.", "Wysoka aktywność fizyczna.", "Bardzo wysoka aktywność fizyczna."};
        final List<String> aktlist = new ArrayList<>(Arrays.asList(Aktywnosc));
        final ArrayAdapter<String> spinnerArrayAdapter = new ArrayAdapter<>(this, R.layout.support_simple_spinner_dropdown_item, aktlist);
        spinnerAkt.setAdapter(spinnerArrayAdapter);

        String[] Plec = new String[]{"Kobieta.", "Mężczyzna."};
        final List<String> plclist = new ArrayList<>(Arrays.asList(Plec));
        final ArrayAdapter<String> spinnerArrayAdapter2 = new ArrayAdapter<>(this, R.layout.support_simple_spinner_dropdown_item, plclist);
        spinnerPlec.setAdapter(spinnerArrayAdapter2);

        btnObl.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                int waga = Integer.parseInt(editTextWag.getText().toString());
                int wzrost = Integer.parseInt(editTextWzr.getText().toString());
                int wiek = Integer.parseInt(editTextWiek.getText().toString());
                double aktywnosc;
                int plec;
                double wynik=0;

                if (spinnerAkt.getSelectedItemPosition()==0){
                    aktywnosc=1.2;
                    if(spinnerPlec.getSelectedItemPosition()==0){
                        wynik=(655 + ((9.6*waga) + (1.8*wzrost)) - (4.7 * wiek))*aktywnosc;
                    }else if(spinnerPlec.getSelectedItemPosition()==1){
                        wynik=(66+((13.7*waga)+(5*wzrost))-(6.76*wiek))*aktywnosc;
                    }
                }
                if (spinnerAkt.getSelectedItemPosition()==1){
                    aktywnosc=1.35;
                    if(spinnerPlec.getSelectedItemPosition()==0){
                        wynik=(655 + ((9.6*waga) + (1.8*wzrost)) - (4.7 * wiek))*aktywnosc;
                    }else if(spinnerPlec.getSelectedItemPosition()==1){
                        wynik=(66+((13.7*waga)+(5*wzrost))-(6.76*wiek))*aktywnosc;
                    }
                }
                if (spinnerAkt.getSelectedItemPosition()==2){
                    aktywnosc=1.55;
                    if(spinnerPlec.getSelectedItemPosition()==0){
                        wynik=(655 + ((9.6*waga) + (1.8*wzrost)) - (4.7 * wiek))*aktywnosc;
                    }else if(spinnerPlec.getSelectedItemPosition()==1){
                        wynik=(66+((13.7*waga)+(5*wzrost))-(6.76*wiek))*aktywnosc;
                    }
                }
                if (spinnerAkt.getSelectedItemPosition()==3){
                    aktywnosc=1.75;
                    if(spinnerPlec.getSelectedItemPosition()==0){
                        wynik=(655 + ((9.6*waga) + (1.8*wzrost)) - (4.7 * wiek))*aktywnosc;
                    }else if(spinnerPlec.getSelectedItemPosition()==1){
                        wynik=(66+((13.7*waga)+(5*wzrost))-(6.76*wiek))*aktywnosc;
                    }
                }
                if (spinnerAkt.getSelectedItemPosition()==4){
                    aktywnosc=2.05;
                    if(spinnerPlec.getSelectedItemPosition()==0){
                        wynik=(655 + ((9.6*waga) + (1.8*wzrost)) - (4.7 * wiek))*aktywnosc;
                    }else if(spinnerPlec.getSelectedItemPosition()==1){
                        wynik=(66+((13.7*waga)+(5*wzrost))-(6.76*wiek))*aktywnosc;

                    }
                }
                textZapo.setText(wynik+"");


            }
        });

    }
}
